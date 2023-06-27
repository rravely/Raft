using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedItem : MonoBehaviour
{
    public Item selectedItem;

    BuildingManager buildingManager;
    BuildingFoundationManager buildingFoundationManager;
    ItemManager itemManager;
    PlayerInteraction playerInteraction;

    [SerializeField] GameObject hammer;
    [SerializeField] GameObject hook;
    [SerializeField] GameObject rope;

    private void Start()
    {
        buildingManager = FindObjectOfType<BuildingManager>();
        buildingFoundationManager = FindObjectOfType<BuildingFoundationManager>();
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
                case "Foundation":
                    buildingFoundationManager.placeNow = true;
                    break;
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
