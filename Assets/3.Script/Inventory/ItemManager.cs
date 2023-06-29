using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [Header("Quick Slots")]
    public Slot[] slots;
    [Header("All Slots")]
    public Slot[] allSlots;

    [Header("Slot Item Prefab")]
    public GameObject slotItemPrefab;

    [Header("Player")]
    [SerializeField] PlayerState playerState;

    int maxStackedCount = 9;

    public bool AddItem(Item item)
    {
        for (int i = 0; i < slots.LongLength; i++)
        {
            Slot slot = slots[i];
            SlotItem itemInSlot = slot.GetComponentInChildren<SlotItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.item.stackable && itemInSlot.count <= maxStackedCount)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                
                playerState.AddPlayerItems(item);
                return true;
            }
        }

        for (int i = 0; i < slots.LongLength; i++)
        {
            Slot slot = slots[i];
            SlotItem itemInSlot = slot.GetComponentInChildren<SlotItem>();
            if (itemInSlot == null)
            {
                SpawnItem(item, slot);
                playerState.AddPlayerItems(item);
                return true;
            }
        }
        return false;
    }

    void SpawnItem(Item item, Slot slot)
    {
        GameObject newItemGo = Instantiate(slotItemPrefab, slot.transform);
        SlotItem slotItem = newItemGo.GetComponent<SlotItem>();
        slotItem.InitialiseItem(item);

        if (!item.isTool)
        {
            slotItem.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            slotItem.transform.GetComponentInChildren<Slider>().value = 1;
            
        }
    }

    public bool RemoveItem(Item item, int count)
    {
        for (int i = 0; i < allSlots.LongLength; i++)
        {
            Slot slot = allSlots[i];
            SlotItem itemInSlot = slot.GetComponentInChildren<SlotItem>();
            if (itemInSlot != null && itemInSlot.item == item)
            {
                itemInSlot.count -= count;
                itemInSlot.RefreshCount();

                playerState.RemovePlayerItems(item, count);
                
                return true;
            }
        }
        return false;
    }

    public void ChangeItem(Item currentItem, Item displaceItem, int slotNum)
    {
        SlotItem itemInSlot = slots[slotNum].GetComponentInChildren<SlotItem>();
        itemInSlot.InitialiseItem(displaceItem);
        
        playerState.RemovePlayerItems(currentItem, 1);
        playerState.AddPlayerItems(displaceItem);
    }

}
