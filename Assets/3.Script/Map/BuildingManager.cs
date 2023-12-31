using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    PlayerInteraction playerInteraction;

    SelectedItem selectedItem;
    [HideInInspector] public int selectedItemIndex = 0;

    [HideInInspector] public GameObject objectToPlace, tempObject;

    [Header("Buildable Items List")]
    public GameObject[] buildableObject;

    [Header("Materials")]
    [SerializeField] Material temp;
    [SerializeField] Material tempDisable;

    [HideInInspector] public Vector3 place;
    [HideInInspector] public bool placeNow = false;
    [HideInInspector] public bool placeObject = false;
    [HideInInspector] public bool tempObjectExists;

    //Raycast
    int layerMaskFoundation;

    // Start is called before the first frame update
    void Start()
    {
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        selectedItem = FindObjectOfType<SelectedItem>();

        layerMaskFoundation = 1 << LayerMask.NameToLayer("Foundation");
    }

    // Update is called once per frame
    void Update()
    {
        if (placeNow)
        {
            SendRay();
            objectToPlace = buildableObject[selectedItemIndex];
        }
    }

    void SendRay()
    {
        if (!selectedItem.selectedItem.isFoundation)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(960, 540)), out RaycastHit hit, 999f, layerMaskFoundation))
            {
                place = new Vector3(hit.point.x, hit.point.y, hit.point.z);

                if (!tempObjectExists)
                {
                    tempObject = Instantiate(buildableObject[selectedItemIndex], place, Quaternion.identity);
                    tempObject.GetComponentInChildren<MeshRenderer>().material = temp;
                    tempObject.GetComponent<Collider>().isTrigger = false;
                    tempObjectExists = true;
                }

                //If player clicks and temp object is buildable, instantiate object.
                if (Input.GetMouseButtonDown(0) && tempObject.GetComponent<BuildableItem>().isBuildable)
                {
                    playerInteraction.InteractionHands();
                    PlayerAudio.instance.PlaceObject();

                    GameObject buildableItem = Instantiate(objectToPlace, place, /*Quaternion.identity*/ tempObject.transform.rotation);
                    buildableItem.GetComponent<BuildableItem>().isBuilt = true;
                    buildableItem.transform.SetParent(transform);
                    placeNow = false;
                    placeObject = false;

                    //Delete item in quickslot
                    selectedItem.RemoveSelectedItem();

                    DestoryTempObject();
                }

                //Rotate temp Object
                if (Input.GetKeyDown(KeyCode.R))
                {
                    tempObject.transform.Rotate(0f, 90f, 0f);
                }

                //Temp object follows aim.
                //Check possibility of building and change material.
                if (tempObject != null)
                {

                    if (!tempObject.GetComponent<BuildableItem>().isBuildable)
                    {
                        //material ����
                        tempObject.GetComponentInChildren<MeshRenderer>().material = tempDisable;
                    }
                    else
                    {
                        tempObject.GetComponentInChildren<MeshRenderer>().material = temp;
                    }

                    tempObject.transform.position = place;
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
