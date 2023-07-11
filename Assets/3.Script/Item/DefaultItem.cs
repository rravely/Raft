using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultItem : MonoBehaviour
{
    ItemManager itemManager;

    [Header("Default Items")]
    [SerializeField] Item[] items;

    [Header("Plank")]
    [SerializeField] Item plank;

    [Header("Thatch")]
    [SerializeField] Item thatch;

    [Header("Scrap")]
    [SerializeField] Item scrap;

    [Header("plastic")]
    [SerializeField] Item plastic;

    [Header("Nail")]
    [SerializeField] Item nail;

    [Header("Rope")]
    [SerializeField] Item rope;

    private void Start()
    {
        itemManager = GetComponent<ItemManager>();
        /*
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                itemManager.AddItem(items[i]);
            }
        }
        */
        itemManager.AddItem(items[0]);
        itemManager.AddItem(items[1]);
        itemManager.AddItem(items[2]);
        itemManager.AddItem(items[3]);
        itemManager.AddItem(items[4]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            itemManager.AddItem(plank);
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            itemManager.AddItem(thatch);
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            itemManager.AddItem(plastic);
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            itemManager.AddItem(scrap);
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            itemManager.AddItem(nail);
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            itemManager.AddItem(rope);
        }
    }
}
