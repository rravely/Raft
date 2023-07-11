using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Craft3Manager : MonoBehaviour
{
    [SerializeField] GameObject[] slots;
    [SerializeField] RectTransform imagePanel;
    [SerializeField] Button craftBtn;
    [SerializeField] Text titleItemName;
    [SerializeField] Text titleItemDescription;

    public Item selectedItem;
    ItemInfo item;
    List<string> itemNameList;
    List<int> itemCountList;

    PlayerState playerState;
    ItemManager itemManager;

    void Start()
    {
        playerState = FindObjectOfType<PlayerState>();
        itemManager = FindObjectOfType<ItemManager>();
    }
    

    private void SetCraft3ItemSlotInactive()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetActive(false);
        }
    }

    public void SetCraft3Panel()
    {
        //ItemSlotCraft2 set active as items's length
        SetCraft3ItemSlotInactive();

        
        //Get information from item database
        int itemIndex = ItemDatabase.instance.FindIndexOfDB(selectedItem);
        item = ItemDatabase.instance.items[itemIndex];

        //View item info
        titleItemName.text = item.item.itemKoreanName;
        titleItemDescription.text = item.item.itemDescription;

        //View Item list
        
        for (int j = 0; j < item.resourcesItems.Length; j++)
        {
            slots[j].SetActive(true);
            slots[j].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = item.resourcesItems[j].icon;
            slots[j].transform.GetChild(2).GetComponent<Text>().text = item.resourcesItems[j].itemKoreanName;
            //Compare player item count between inventory info and craft info
        }

        UpdateItemList(item);
        ChangeCraftBtn(CheckCraftItem(item));
        
        //Modify Craft2Panel Size 
        imagePanel.offsetMin = new Vector2(0, 300 - 65 * (item.resourcesItems.Length - 1));

        /*
        //Get information from item database
        ItemSQLManager.instance.FindResourceItems(selectedItem);
        itemNameList = ItemSQLManager.instance.craftItem.itemNameList;
        itemCountList = ItemSQLManager.instance.craftItem.itemCountList;

        //View item info
        titleItemName.text = selectedItem.itemKoreanName;
        titleItemDescription.text = selectedItem.itemDescription;

        //View Item list
        for (int j = 0; j < itemNameList.Count; j++)
        {
            slots[j].SetActive(true);
            slots[j].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = ItemDatabase.instance.FindItem(itemNameList[j]).icon;
            slots[j].transform.GetChild(2).GetComponent<Text>().text = ItemDatabase.instance.FindItem(itemNameList[j]).itemKoreanName;
        }

        UpdateItemList();
        ChangeCraftBtn(CheckCraftItem());
        
        //Modify Craft2Panel Size 
        imagePanel.offsetMin = new Vector2(0, 300 - 65 * (itemNameList.Count - 1));
        */
    }

    public bool CheckCraftItem(ItemInfo item)
    {
        int count = 0;
        for (int i = 0; i < item.resourcesItems.Length; i++)
        {
            if (playerState.FindItemCount(item.resourcesItems[i]) >= item.resourcesItemCount[i])
            {
                count++;
            }
        }
        if (count.Equals(item.resourcesItems.Length))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChangeCraftBtn(bool isCraftPossible)
    {
        if (isCraftPossible)
        {
            craftBtn.interactable = true;
        }
        else
        {
            craftBtn.interactable = false;
        }
    }

    public void CraftItem()
    {
        for (int i = 0; i < item.resourcesItems.Length; i++)
        {
            itemManager.RemoveItem(item.resourcesItems[i], item.resourcesItemCount[i]);
        }
        itemManager.AddItem(item.item);
        UpdateItemList(item);
    }

    void UpdateItemList(ItemInfo item)
    {
        for (int j = 0; j < item.resourcesItems.Length; j++)
        {
            slots[j].transform.GetChild(3).GetComponent<Text>().text = string.Format("{0}/{1}", playerState.FindItemCount(item.resourcesItems[j]), item.resourcesItemCount[j]);
        }
    }
}
