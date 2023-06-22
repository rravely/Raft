using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultItem : MonoBehaviour
{
    ItemManager itemManager;

    [Header("Default Items")]
    [SerializeField] Item[] items;

    private void Start()
    {
        itemManager = GetComponent<ItemManager>();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                itemManager.AddItem(items[i]);
            }
        }

        itemManager.AddItem(items[3]);
        itemManager.AddItem(items[4]);
        itemManager.AddItem(items[5]);
    }
}
