using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedItem : MonoBehaviour
{
    public Item selectedItem;
    public int selectedButtonIndex;

    BuildingManager buildingManager;
    BuildingFoundationManager buildingFoundationManager;
    BuildingPillarManager buildingPillarManager;
    BuildingFloorManager buildingFloorManager;
    BuildingStairsManager buildingStairsManager;

    ItemManager itemManager;
    PlayerInteraction playerInteraction;

    [Header("Tools")]
    [SerializeField] GameObject hammer;
    [SerializeField] GameObject hook;
    [SerializeField] GameObject rope;

    [Header("Hands")]
    [SerializeField] GameObject defaultHands;
    [SerializeField] GameObject rodHands;

    private void Start()
    {
        buildingManager = FindObjectOfType<BuildingManager>();
        buildingFoundationManager = FindObjectOfType<BuildingFoundationManager>();
        buildingPillarManager = FindObjectOfType<BuildingPillarManager>();
        buildingFloorManager = FindObjectOfType<BuildingFloorManager>();
        buildingStairsManager = FindObjectOfType<BuildingStairsManager>();

        itemManager = FindObjectOfType<ItemManager>();
        playerInteraction = FindObjectOfType<PlayerInteraction>();
    }

    private void Update()
    {
        SetActiveSelectedItem();
    }

    void SetActiveSelectedItem()
    {
        if (selectedItem.itemName.Equals("FishingRod"))
        {
            defaultHands.SetActive(false);
            rodHands.SetActive(true);
        }
        else
        {
            defaultHands.SetActive(true);
            rodHands.SetActive(false);
        }

        if (!selectedItem.isBuildable)
        {
            NoBuildableItem();

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
                NoTool();
            }
        }
        else //buildable Items
        {
            hammer.SetActive(true);
            hook.SetActive(false);
            rope.SetActive(false);
            playerInteraction.GrabHands();

            if (!selectedItem.isFoundation) 
            {
                buildingFoundationManager.placeNow = false;
                buildingPillarManager.placeNow = false;
                buildingFloorManager.placeNow = false;

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
            else //foundation
            {
                buildingManager.placeNow = false;

                switch (selectedItem.itemName)
                {
                    case "Foundation":
                        buildingFoundationManager.placeNow = true;
                        buildingPillarManager.placeNow = false;
                        buildingFloorManager.placeNow = false;
                        buildingStairsManager.placeNow = false;

                        buildingFoundationManager.selectedItemIndex = 0;
                        break;
                    case "WoodenFloor":
                        buildingFloorManager.placeNow = true;
                        buildingFoundationManager.placeNow = false;
                        buildingPillarManager.placeNow = false;
                        buildingStairsManager.placeNow = false;

                        buildingFoundationManager.selectedItemIndex = 0;
                        break;
                    case "Pillar":
                        buildingPillarManager.placeNow = true;
                        buildingFoundationManager.placeNow = false;
                        buildingFloorManager.placeNow = false;
                        buildingStairsManager.placeNow = false;

                        buildingPillarManager.selectedItemIndex = 0;
                        break;
                    case "Stairs":
                        buildingStairsManager.placeNow = true;
                        buildingFoundationManager.placeNow = false;
                        buildingPillarManager.placeNow = false;
                        buildingFloorManager.placeNow = false;

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

    void NoBuildableItem()
    {
        buildingManager.placeNow = false;
        buildingManager.DestoryTempObject();

        buildingFoundationManager.placeNow = false;
        buildingFoundationManager.DestoryTempObject();

        buildingPillarManager.placeNow = false;
        buildingPillarManager.DestoryTempObject();

        buildingFloorManager.placeNow = false;
        buildingFloorManager.DestoryTempObject();

        buildingStairsManager.placeNow = false;
        buildingStairsManager.DestoryTempObject();
    }

    void NoTool()
    {
        hammer.SetActive(false);
        hook.SetActive(false);
        rope.SetActive(false);
        playerInteraction.PlayerIdle(true);
    }
}
