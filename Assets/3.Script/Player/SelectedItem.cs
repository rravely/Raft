using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedItem : MonoBehaviour
{
    public Item selectedItem;
    public int selectedButtonIndex;

    BuildingManager buildingManager;
    StructureManager structureManager;
    ItemManager itemManager;

    PlayerInteraction playerInteraction;

    [Header("Tools")]
    [SerializeField] GameObject hammer;
    [SerializeField] GameObject hook;
    [SerializeField] GameObject rope;

    [Header("Hands")]
    [SerializeField] GameObject defaultHands;
    [SerializeField] GameObject rodHands;

    [Header("StructureMenu")]
    [SerializeField] GameObject structureMenu;

    private void Start()
    {
        buildingManager = FindObjectOfType<BuildingManager>();
        structureManager = FindObjectOfType<StructureManager>();

        itemManager = FindObjectOfType<ItemManager>();
        playerInteraction = FindObjectOfType<PlayerInteraction>();
    }

    private void Update()
    {
        SetActiveSelectedItem();
    }

    void SetActiveSelectedItem()
    {
        //Fishing
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

        if (selectedItem.id.Equals(0)) //nullitem
        {
            playerInteraction.PlayerIdle(true);
        }

        if (!selectedItem.isBuildable)
        {
            NoBuildableItem();

            if (selectedItem.isTool)
            {
                playerInteraction.PlayerIdle(false);
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
            playerInteraction.GrabHands(true);

            if (!selectedItem.isFoundation) 
            {
                structureManager.placeNow = false;
                structureManager.DestoryTempObject();
                

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
                structureManager.placeNow = true;
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
    }

    void NoTool()
    {
        hook.SetActive(false);
        rope.SetActive(false);
        playerInteraction.PlayerIdle(true);
    }

    public void SetActiveHammer(bool isActive)
    {
        hammer.SetActive(isActive);
    }

    public void SetActiveHook(bool isActive)
    {
        hook.SetActive(isActive);
        rope.SetActive(isActive);
    }
}
