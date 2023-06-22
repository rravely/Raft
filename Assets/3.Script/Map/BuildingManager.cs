using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
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
                    tempObjectExists = true;
                }

                //Rotate temp Object

                //If player clicks and temp object is buildable, instantiate object.
                if (Input.GetMouseButtonDown(0) && tempObject.GetComponent<BuildableItem>().isBuildable)
                {
                    Instantiate(objectToPlace, place, Quaternion.identity);
                    placeNow = false;
                    placeObject = false;

                    //Delete item in quickslot
                    selectedItem.RemoveSelectedItem();

                    DestoryTempObject();
                }

                //Temp object follows aim.
                //Check possibility of building and change material.
                if (tempObject != null)
                {

                    if (!tempObject.GetComponent<BuildableItem>().isBuildable)
                    {
                        //material º¯°æ
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
