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

            if (selectedItem.isTool) //hook or fishingrod
            {
                playerInteraction.PlayerIdle(false);
                switch (selectedItem.itemName)
                {
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
                if (selectedItem.itemName.Equals("Hammer"))
                {
                    hammer.SetActive(true);
                }
                else
                {
                    hammer.SetActive(false);   
                }
            }
        }
        else //buildable Items
        {
            //interaction hands
            hammer.SetActive(false);
            hook.SetActive(false);
            rope.SetActive(false);
            playerInteraction.PlayerIdle(true);

            if (!selectedItem.isFoundation) 
            {
                structureManager.placeNow = false;
                structureManager.DestoryTempObject();
                
                switch (selectedItem.itemName)
                {
                    case "BedSimple":
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
