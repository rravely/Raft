using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StructureManager : MonoBehaviour
{
    PlayerInteraction playerInteraction;

    //SelectedItem selectedItem;
    [HideInInspector] public int selectedItemIndex = 0;

    [HideInInspector] public GameObject objectToPlace, tempObject;
    Item tempItem;

    [Header("Buildable Items List")]
    public GameObject[] structureObject;

    [Header("Materials")]
    [SerializeField] Material temp;
    [SerializeField] Material tempDisable;

    Vector3 place;
    Vector3 tilemapPlace;
    [HideInInspector] public bool placeNow = false;
    [HideInInspector] public bool placeObject = false;
    [HideInInspector] public bool tempObjectExists;

    //For selected sturcture item
    public Item selectedStructureItem;
    bool isSelected = false;

    //Raycast (foundation -> pillar -> floor -> stairs)
    int[] layerMasks;
    int layerMaskIndex = 0;

    [Header("Tilemap (foundation -> pillar -> floor -> stairs)")]
    [SerializeField] Tilemap[] tilemaps;
    [SerializeField] Transform[] parents;
    Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        //selectedItem = FindObjectOfType<SelectedItem>();

        layerMasks = new int[4];
        layerMasks[0] = 1 << LayerMask.NameToLayer("Foundation");
        layerMasks[1] = 1 << LayerMask.NameToLayer("FoundationPlane");
        layerMasks[2] = 1 << LayerMask.NameToLayer("FloorPlane");
        layerMasks[3] = 1 << LayerMask.NameToLayer("Foundation");
    }

    // Update is called once per frame
    void Update()
    {
        if (placeNow)
        {
            SendRay();
            objectToPlace = structureObject[layerMaskIndex];
        }
        else
        {
            DestoryTempObject();
        }
    }

    void SendRay()
    {
        SetLayerMask();

        if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(960, 540)), out RaycastHit hit, 999f, layerMasks[layerMaskIndex]))
        {
            place = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            tilemapPlace = tilemaps[layerMaskIndex].GetCellCenterWorld(tilemaps[layerMaskIndex].WorldToCell(place));

            if (!tempObjectExists)
            {
                InstantiateTempObject();
            }
            else if (tempObjectExists && !tempItem.Equals(selectedStructureItem))
            {
                DestoryTempObject();
                InstantiateTempObject();
            }

            //If player clicks and temp object is buildable, instantiate object.
            if (Input.GetMouseButtonDown(0))
            {
                InstantiateObject();
            }

            //Rotate temp Object
            if (Input.GetKeyDown(KeyCode.R))
            {
                tempObject.transform.Rotate(0f, 90f, 0f);
            }

            //Check possibility of building and change material.
            ChangeMaterial();

        }

    }

    void SetLayerMask()
    {
        switch (selectedStructureItem.itemName)
        {
            case "Foundation":
                layerMaskIndex = 0;
                break;
            case "Pillar":
                layerMaskIndex = 1;
                break;
            case "WoodenFloor":
                layerMaskIndex = 2;
                break;
            case "Stairs":
                layerMaskIndex = 3;
                break;
            default:
                layerMaskIndex = 0;
                break;
        }
        CheckResourcesItemCount();
    }

    void InstantiateTempObject()
    {
        tempObject = Instantiate(structureObject[layerMaskIndex], tilemapPlace, Quaternion.identity);
        tempObject.GetComponentInChildren<MeshRenderer>().material = temp;
        tempObject.GetComponent<Collider>().isTrigger = true;
        tempObjectExists = true;
        tempItem = selectedStructureItem;
    }

    void InstantiateObject()
    {
        playerInteraction.HammerHands();
        PlayerAudio.instance.PlaceObject();

        parent = parents[layerMaskIndex];

        switch (selectedStructureItem.itemName)
        {
            case "Foundation":
                if (!tempObject.GetComponent<Foundation>().isExist && tempObject.GetComponent<Foundation>().isBuildable)
                {
                    GameObject foundation = Instantiate(objectToPlace, tilemapPlace, tempObject.transform.rotation);
                    foundation.transform.SetParent(parent);
                    foundation.GetComponent<Foundation>().isBuild = true;
                    placeNow = false;
                    placeObject = false;

                    DestoryTempObject();
                }
                break;
            case "Pillar":
                if (!tempObject.GetComponent<Pillar>().isExist && tempObject.GetComponent<Pillar>().isBuildable)
                {
                    GameObject pillar = Instantiate(objectToPlace, tilemapPlace, Quaternion.identity);
                    pillar.transform.SetParent(parent);
                    pillar.GetComponent<Pillar>().isBuild = true;
                    placeNow = false;
                    placeObject = false;

                    DestoryTempObject();
                }
                break;
            case "WoodenFloor":
                if (!tempObject.GetComponent<Floor>().isExist && tempObject.GetComponent<Floor>().isBuildable)
                {
                    GameObject foundation = Instantiate(objectToPlace, tilemapPlace, tempObject.transform.rotation);
                    foundation.transform.SetParent(parent);
                    foundation.GetComponent<Floor>().isBuild = true;
                    placeNow = false;
                    placeObject = false;

                    DestoryTempObject();
                }
                break;
            case "Stairs":
                if (!tempObject.GetComponentInChildren<Stairs>().isExist && tempObject.GetComponentInChildren<Stairs>().isBuildable)
                {
                    GameObject foundation = Instantiate(objectToPlace, tilemapPlace, tempObject.transform.rotation);
                    foundation.transform.SetParent(parent);
                    foundation.GetComponentInChildren<Stairs>().isBuild = true;
                    placeNow = false;
                    placeObject = false;

                    DestoryTempObject();
                }
                break;
        }
    }

    void ChangeMaterial()
    {
        switch (selectedStructureItem.itemName)
        {
            case "Foundation":
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
                }
                break;

            case "Pillar":
                if (tempObject != null)
                {
                    if (!tempObject.GetComponent<Pillar>().isExist && tempObject.GetComponent<Pillar>().isBuildable)
                    {
                        tempObject.GetComponent<MeshRenderer>().material = temp;
                    }
                    else
                    {
                        tempObject.GetComponent<MeshRenderer>().material = tempDisable;
                    }
                }
                break;
            case "WoodenFloor":
                if (tempObject != null)
                {
                    if (!tempObject.GetComponent<Floor>().isExist && tempObject.GetComponent<Floor>().isBuildable)
                    {
                        tempObject.GetComponent<MeshRenderer>().material = temp;
                    }
                    else
                    {
                        tempObject.GetComponent<MeshRenderer>().material = tempDisable;
                    }
                }
                break;
            case "Stairs":
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
                }
                break;
        }
        tempObject.transform.position = tilemaps[layerMaskIndex].GetCellCenterWorld(tilemaps[layerMaskIndex].WorldToCell(place));
    }

    public void DestoryTempObject()
    {
        if (tempObject != null)
        {
            Destroy(tempObject);
            tempObjectExists = false;
        }
    }

    bool CheckResourcesItemCount()
    {


        return false;
    }
}
