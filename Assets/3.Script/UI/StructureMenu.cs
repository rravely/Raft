using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureMenu : MonoBehaviour
{
    SelectedItem selectedItem;
    PlayerInput playerInput;
    PlayerState playerState;

    StructureManager structureManager;

    [SerializeField] GameObject structureMenu;

    private void Start()
    {
        selectedItem = FindObjectOfType<SelectedItem>();
        playerInput = FindObjectOfType<PlayerInput>();
        playerState = FindObjectOfType<PlayerState>();

        structureManager = GetComponent<StructureManager>();
    }

    private void Update()
    {
        //Open and close structure menu
        if (selectedItem.selectedItem.itemName.Equals("Hammer") && !playerState.inWater && !playerState.inWaterSurface)
        {
            structureManager.placeNow = true;
            if (playerInput.isRMD)
            {
                structureMenu.SetActive(true);
                structureManager.DestoryTempObject();
            }
            else
            {
                structureMenu.SetActive(false);
            }
        }
        else
        {
            structureManager.placeNow = false;
        }
    }
}
