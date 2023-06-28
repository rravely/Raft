using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingStairsManager : MonoBehaviour
{
    PlayerInteraction playerInteraction;

    SelectedItem selectedItem;
    [HideInInspector] public int selectedItemIndex = 0;

    public GameObject objectToPlace, tempObject;

    [Header("Buildable Items List")]
    public GameObject[] foundationObject;

    [Header("Materials")]
    [SerializeField] Material temp;
    [SerializeField] Material tempDisable;

    [HideInInspector] Vector3 place;
    [HideInInspector] Vector3 tilemapPlace;
    [HideInInspector] public bool placeNow = false;
    [HideInInspector] public bool placeObject = false;
    [HideInInspector] public bool tempObjectExists;

    //Raycast
    int layerMaskFoundation;

    //Grid
    Tilemap tilemap;


    // Start is called before the first frame update
    void Start()
    {
        playerInteraction = FindObjectOfType<PlayerInteraction>();

        selectedItem = FindObjectOfType<SelectedItem>();
        tilemap = GetComponentInChildren<Tilemap>();

        layerMaskFoundation = 1 << LayerMask.NameToLayer("Foundation");
    }

    // Update is called once per frame
    void Update()
    {
        if (placeNow)
        {
            SendRay();
            objectToPlace = foundationObject[selectedItemIndex];
        }
    }

    void SendRay()
    {
        if (selectedItem.selectedItem.isFoundation)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(960, 540)), out RaycastHit hit, 999f, layerMaskFoundation))
            {
                place = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                tilemapPlace = tilemap.GetCellCenterWorld(tilemap.WorldToCell(place));
                //Debug.Log(tilemap.WorldToCell(place));

                if (!tempObjectExists)
                {
                    //Instantiate foundation temp object
                    tempObject = Instantiate(foundationObject[selectedItemIndex], tilemapPlace, Quaternion.identity);
                    tempObject.GetComponentInChildren<MeshRenderer>().material = temp;
                    tempObjectExists = true;
                }

                //If player clicks, instantiate object.
                //Must check ispossible
                if (Input.GetMouseButtonDown(0) && !tempObject.GetComponentInChildren<Stairs>().isExist && tempObject.GetComponentInChildren<Stairs>().isBuildable)
                {
                    playerInteraction.Hammer();

                    GameObject foundation = Instantiate(objectToPlace, tilemapPlace, /*Quaternion.identity*/ tempObject.transform.rotation);
                    foundation.transform.SetParent(transform);
                    foundation.GetComponentInChildren<Stairs>().isBuild = true;
                    placeNow = false;
                    placeObject = false;

                    //Delete item in quickslot
                    selectedItem.RemoveSelectedItem();

                    DestoryTempObject();
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    tempObject.transform.Rotate(0f, 90f, 0f);
                }

                //Debug.Log($"{tempObject.GetComponent<Foundation>().isExist}, {tempObject.GetComponent<Foundation>().isBuildable}");

                if (tempObject != null)
                {
                    if (!tempObject.GetComponentInChildren<Stairs>().isExist && tempObject.GetComponentInChildren<Stairs>().isBuildable)
                    {
                        tempObject.GetComponentInChildren<MeshRenderer>().material = temp;
                    }
                    else
                    {
                        tempObject.GetComponentInChildren<MeshRenderer>().material = tempDisable;
                    }
                    tempObject.transform.position = tilemap.GetCellCenterWorld(tilemap.WorldToCell(place));
                }

            }
        }
    }

    public void DestoryTempObject()
    {
        if (tempObject != null)
        {
            Destroy(tempObject);
            tempObjectExists = false;
        }
    }
}
