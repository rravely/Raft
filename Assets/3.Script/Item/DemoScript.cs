using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    private ItemManager itemManager;
    public Item[] itemToPickup;

    private void Awake()
    {
        itemManager = FindAnyObjectByType<ItemManager>();
    }

    public void PickupItem(int id)
    {
        bool result = itemManager.AddItem(itemToPickup[id]);
        if (result)
        {
            Debug.Log("Add Item");
        }
        else
        {
            Debug.Log("Add Item fail");
        }
    }
}