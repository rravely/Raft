using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;

using System.IO; //Input Ouput
using System;

using LitJson;

public class CraftResourceItems
{
    public string itemName { get; private set; }
    public Dictionary<string, int> resourceItemList { get; private set; }

    public CraftResourceItems(string itemName)
    {
        this.itemName = itemName;
        resourceItemList = new Dictionary<string, int>();
    }

    public void AddItemInList(string resourceItemName, int count)
    {
        resourceItemList.Add(resourceItemName, count);
    }
}

public class ItemSQLManager : MonoBehaviour
{
    public MySqlConnection connection;
    public MySqlDataReader reader;

    public string DBPath = string.Empty;


}
