using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StructureMenu : MonoBehaviour
{
    SelectedItem selectedItem;
    PlayerInput playerInput;
    PlayerState playerState;

    StructureManager structureManager;
    int index = 1000;

    StructureItemDatabase itemDB;

    [Header("UI")]
    [SerializeField] GameObject structureMenu;
    [SerializeField] GameObject structureResourcesUI;
    [SerializeField] GameObject interactionButton;

    [Header("Sprite")]
    [SerializeField] Sprite trueSprite;
    [SerializeField] Sprite falseSprite;

    [Header("Resources item slot")]
    [SerializeField] Transform[] itemSlots;

    private void Start()
    {
        selectedItem = FindObjectOfType<SelectedItem>();
        playerInput = FindObjectOfType<PlayerInput>();
        playerState = FindObjectOfType<PlayerState>();
        itemDB = FindObjectOfType<StructureItemDatabase>();

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
                SetUI(true);
                structureManager.DestoryTempObject();
            }
            else
            {
                SetUI(false);
                SetResources();
            }
        }
        else
        {
            structureManager.placeNow = false;
            structureResourcesUI.SetActive(false);
            interactionButton.SetActive(false);
        }
    }

    void SetUI(bool isActive)
    {
        structureMenu.SetActive(isActive);
        structureResourcesUI.SetActive(!isActive);
        interactionButton.SetActive(!isActive);
    }

    void SetResources()
    {
        index = itemDB.FindIndexOfDB(structureManager.selectedStructureItem);
        if (itemDB.structureItems[index].resourcesItems.Length < 2)
        {
            itemSlots[1].gameObject.SetActive(false);
            SetSlotPanel(0);
        }
        else
        {
            itemSlots[1].gameObject.SetActive(true);
            SetSlotPanel(0);
            SetSlotPanel(1);
        }
    }

    void SetSlotPanel(int panelIndex)
    {
        itemSlots[panelIndex].GetChild(0).GetComponent<Image>().sprite = itemDB.structureItems[index].resourcesItems[panelIndex].icon;
        itemSlots[panelIndex].GetChild(1).GetComponent<Text>().text = string.Format($"{playerState.FindItemCount(itemDB.structureItems[index].resourcesItems[panelIndex])}/{itemDB.structureItems[index].resourcesItemCount[panelIndex]}");
        if (playerState.FindItemCount(itemDB.structureItems[index].resourcesItems[panelIndex]) >= itemDB.structureItems[index].resourcesItemCount[panelIndex])
        {
            itemSlots[panelIndex].GetComponent<Image>().sprite = trueSprite;
        }
        else
        {
            itemSlots[panelIndex].GetComponent<Image>().sprite = falseSprite;
        }
    }
}
