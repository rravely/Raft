using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingFoundationManager : MonoBehaviour
{
    PlayerInteraction playerInteraction;

    SelectedItem selectedItem;
    [HideInInspector] public int selectedItemIndex = 0;

    [HideInInspector] public GameObject objectToPlace, tempObject;

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

        layerMaskFoundation = 1 << LayerMask.NameToLayer("FoundationPlane");
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
                    tempObject.GetComponent<MeshRenderer>().material = temp;
                    tempObject.GetComponent<Collider>().isTrigger = true;
                    tempObjectExists = true;
                }

                //If player clicks, instantiate object.
                //Must check ispossible
                if (Input.GetMouseButtonDown(0))
                {
                    CreateObject();
                }

                //Debug.Log($"{tempObject.GetComponent<Foundation>().isExist}, {tempObject.GetComponent<Foundation>().isBuildable}");

                if (tempObject != null)
                {
                    if (!tempObject.GetComponent<Foundation>().isExist && tempObject.GetComponent<Foundation>().isBuildable)
                    {
                        tempObject.GetComponent<MeshRenderer>().material = temp;
                    }
                    else
                    {
                        tempObject.GetComponent<MeshRenderer>().material = tempDisable;
                    }
                    tempObject.transform.position = tilemap.GetCellCenterWorld(tilemap.WorldToCell(place));
                }

            }
        }
    }

    void CreateObject()
    {
        PlayerAudio.instance.PlaceObject();
        switch (selectedItemIndex)
        {
            case 0: //foundation
                if (!tempObject.GetComponent<Foundation>().isExist && tempObject.GetComponent<Foundation>().isBuildable)
                {
                    playerInteraction.Hammer();

                    GameObject foundation = Instantiate(objectToPlace, tilemapPlace, Quaternion.identity);
                    foundation.transform.SetParent(transform);
                    foundation.GetComponent<Foundation>().isBuild = true;
                    placeNow = false;
                    placeObject = false;

                    //Delete item in quickslot
                    selectedItem.RemoveSelectedItem();

                    DestoryTempObject();
                }
                break;
            case 1: 
                break;
            case 2:
                break;
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
