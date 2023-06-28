using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedItem : MonoBehaviour
{
    public Item selectedItem;

    BuildingManager buildingManager;
    BuildingFoundationManager buildingFoundationManager;
    BuildingPillarManager buildingPillarManager;

    ItemManager itemManager;
    PlayerInteraction playerInteraction;

    [SerializeField] GameObject hammer;
    [SerializeField] GameObject hook;
    [SerializeField] GameObject rope;

    private void Start()
    {
        buildingManager = FindObjectOfType<BuildingManager>();
        buildingFoundationManager = FindObjectOfType<BuildingFoundationManager>();
        buildingPillarManager = FindObjectOfType<BuildingPillarManager>();

        itemManager = FindObjectOfType<ItemManager>();
        playerInteraction = FindObjectOfType<PlayerInteraction>();
    }

    private void Update()
    {
        SetActiveSelectedItem();
    }

    void SetActiveSelectedItem()
    {
        if (!selectedItem.isBuildable)
        {
            buildingManager.placeNow = false;
            buildingManager.DestoryTempObject();

            buildingFoundationManager.placeNow = false;
            buildingFoundationManager.DestoryTempObject();

            buildingPillarManager.placeNow = false;
            buildingPillarManager.DestoryTempObject();

            if (selectedItem.isTool)
            {
                playerInteraction.PlayerIdle(false);
                switch (selectedItem.itemName)
                {
                    case "Hammer":
                        hammer.SetActive(true);
                        hook.SetActive(false);
                        rope.SetActive(false);
                        break;
                    case "PlasticHook":
                        hammer.SetActive(false);
                        hook.SetActive(true);
                        rope.SetActive(true);
                        break;
                }
            }
            else
            {
                hammer.SetActive(false);
                hook.SetActive(false);
                rope.SetActive(false);
                playerInteraction.PlayerIdle(true);
            }
        }
        else
        {
            hammer.SetActive(true);
            hook.SetActive(false);
            rope.SetActive(false);
            playerInteraction.GrabHands();

            if (!selectedItem.isFoundation)
            {
                buildingFoundationManager.placeNow = false;
                switch (selectedItem.itemName)
                {
                    case "SimpleBed":
                        BuildableObjectPlaceable(2);
                        break;
                    case "Chair":
                        BuildableObjectPlaceable(0);
                        break;
                    case "Table":
                        BuildableObjectPlaceable(1);
                        break;
                    case "Purifier":
                        BuildableObjectPlaceable(3);
                        break;
                }
            }
            else 
            {
                switch (selectedItem.itemName)
                {
                    case "Foundation":
                        buildingFoundationManager.placeNow = true;
                        buildingPillarManager.placeNow = false;

                        buildingFoundationManager.selectedItemIndex = 0;
                        break;
                    case "WoodenFloor":
                        buildingFoundationManager.placeNow = true;
                        buildingPillarManager.placeNow = false;

                        buildingFoundationManager.selectedItemIndex = 1;
                        break;
                    case "Pillar":
                        buildingFoundationManager.placeNow = false;
                        buildingPillarManager.placeNow = true;
                        break;
                }
            }
        }
    }

    void BuildableObjectPlaceable(int index)
    {
        buildingManager.placeNow = true;
        buildingManager.selectedItemIndex = index;
    }

    public void RemoveSelectedItem()
    {
        itemManager.RemoveItem(selectedItem, 1);
    }    
}
