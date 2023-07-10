using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StructureManager : MonoBehaviour
{
    PlayerInteraction playerInteraction;
    ItemManager itemManager;
    StructureItemDatabase itemDB;

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

    //Raycast (foundation -> pillar -> floor -> stairs)
    int[] layerMasks;
    int layerMaskIndex = 0;
    int tilemapIndex = 0;

    [Header("Tilemap (foundation -> pillar -> floor -> stairs -> wallsX -> wallsZ)")]
    [SerializeField] Tilemap[] tilemaps;
    [SerializeField] Transform[] parents;
    Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        itemManager = FindObjectOfType<ItemManager>();
        itemDB = FindObjectOfType<StructureItemDatabase>();
        //selectedItem = FindObjectOfType<SelectedItem>();

        layerMasks = new int[6];
        layerMasks[0] = 1 << LayerMask.NameToLayer("Foundation");
        layerMasks[1] = 1 << LayerMask.NameToLayer("FoundationPlane");
        layerMasks[2] = 1 << LayerMask.NameToLayer("FloorPlane");
        layerMasks[3] = 1 << LayerMask.NameToLayer("Foundation");
        layerMasks[4] = 1 << LayerMask.NameToLayer("FoundationPlane");
        layerMasks[5] = 1 << LayerMask.NameToLayer("FoundationPlane");
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
            if (selectedStructureItem.itemName.Equals("WoodWall") || selectedStructureItem.itemName.Equals("HalfWoodWall"))
            {
                if (!tempObjectExists)
                {
                    tilemapIndex = 4;
                }
                else
                {
                    if (tempObject.transform.rotation.y.Equals(0) || tempObject.transform.rotation.y.Equals(1))
                    {
                        tilemapIndex = 4;
                    }
                    else
                    {
                        tilemapIndex = 5;
                    }
                }
            }
            tilemapPlace = tilemaps[tilemapIndex].GetCellCenterWorld(tilemaps[tilemapIndex].WorldToCell(place));

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
            if (Input.GetMouseButtonDown(0) && placeObject)
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
                tilemapIndex = 0;
                break;
            case "Pillar":
                layerMaskIndex = 1;
                tilemapIndex = 1;
                break;
            case "WoodenFloor":
                layerMaskIndex = 2;
                tilemapIndex = 2;
                break;
            case "Stairs":
                layerMaskIndex = 3;
                tilemapIndex = 3;
                break;
            case "WoodWall":
                layerMaskIndex = 4;
                tilemapIndex = 4;
                break;
            case "HalfWoodWall":
                layerMaskIndex = 5;
                tilemapIndex = 5;
                break;
            default:
                layerMaskIndex = 0;
                tilemapIndex = 0;
                break;
        }
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
            case "WoodWall":
                Debug.Log("¿©±â");
                if (!tempObject.GetComponentInChildren<Wall>().isExist && tempObject.GetComponentInChildren<Wall>().isBuildable)
                {
                    GameObject foundation = Instantiate(objectToPlace, tilemapPlace, tempObject.transform.rotation);
                    foundation.transform.SetParent(parent);
                    foundation.GetComponentInChildren<Wall>().isBuild = true;
                    placeNow = false;
                    placeObject = false;

                    DestoryTempObject();
                }
                break;
            case "HalfWoodWall":
                if (!tempObject.GetComponentInChildren<Wall>().isExist && tempObject.GetComponentInChildren<Wall>().isBuildable)
                {
                    GameObject foundation = Instantiate(objectToPlace, tilemapPlace, tempObject.transform.rotation);
                    foundation.transform.SetParent(parent);
                    foundation.GetComponentInChildren<Wall>().isBuild = true;
                    placeNow = false;
                    placeObject = false;

                    DestoryTempObject();
                }
                break;
        }

        RemoveResourceItems(selectedStructureItem);
    }

    void ChangeMaterial()
    {
        if (placeObject)
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
                case "WoodWall":
                    if (tempObject != null)
                    {
                        if (!tempObject.GetComponentInChildren<Wall>().isExist && tempObject.GetComponentInChildren<Wall>().isBuildable)
                        {
                            tempObject.GetComponentInChildren<MeshRenderer>().material = temp;
                        }
                        else
                        {
                            tempObject.GetComponentInChildren<MeshRenderer>().material = tempDisable;
                        }
                    }
                    break;
                case "HalfWoodWall":
                    if (tempObject != null)
                    {
                        if (!tempObject.GetComponentInChildren<Wall>().isExist && tempObject.GetComponentInChildren<Wall>().isBuildable)
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
            tempObject.transform.position = tilemaps[tilemapIndex].GetCellCenterWorld(tilemaps[tilemapIndex].WorldToCell(place));
        }
        else
        {
            DestoryTempObject();
        }
    }

    void RemoveResourceItems(Item item)
    {
        int index = itemDB.FindIndexOfDB(item);
        for (int i = 0; i < itemDB.structureItems[index].resourcesItems.Length; i++)
        {
            itemManager.RemoveItem(itemDB.structureItems[index].resourcesItems[i], itemDB.structureItems[index].resourcesItemCount[i]);
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
