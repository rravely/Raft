using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSqlData : MonoBehaviour
{
    [SerializeField] Item item;

    public void LoadData()
    {
        ItemSQLManager.instance.FindResourceItems(item);
        
        Dictionary<string, int> itemList = ItemSQLManager.instance.craftItem.resourceItemList;
        foreach (KeyValuePair<string, int> i in itemList)
        {
            Debug.Log(i.Key + " " + i.Value);
        }
        
    }
}
