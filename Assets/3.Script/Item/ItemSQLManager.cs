using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;

using System.IO; //Input Ouput
using System;
using System.Data;

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

    public static ItemSQLManager instance = null;

    public CraftResourceItems craftItem;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DBPath = Application.dataPath + "/Database";
        string serverInfo = SetServer(DBPath);

        try
        {
            if (serverInfo.Equals(string.Empty))
            {
                Debug.Log("Sql server is not connected");
                return;
            }
            connection = new MySqlConnection(serverInfo);
            connection.Open();
            Debug.Log("Sql open completely");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    string SetServer(string path)
    {
        if (!File.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string jsonString = File.ReadAllText(path + "/config.json");
        JsonData itemData = JsonMapper.ToObject(jsonString);
        string serverInfo = $"Server={itemData[0]["IP"]}; Database={itemData[0]["TableName"]}; Uid={itemData[0]["ID"]}; Pwd={itemData[0]["PW"]}; Port={itemData[0]["PORT"]}; CharSet=utf8;";

        return serverInfo;
    }

    private bool CheckConnection(MySqlConnection c)
    {
        if (c.State != System.Data.ConnectionState.Open)
        {
            c.Open();
            if (c.State != System.Data.ConnectionState.Open)
            {
                return false;
            }
        }
        return true;
    }

    public void FindResourceItems(Item item)
    {
        try
        {
            if (!CheckConnection(connection)) Debug.Log("Connect failed");

            string SQLCommand = string.Format(@"SELECT ResourceItem, Count
            FROM craftresources WHERE CraftItem = '{0}';", item.itemName);

            MySqlCommand cmd = new MySqlCommand(SQLCommand, connection);
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                craftItem = new CraftResourceItems(item.itemName);

                while (reader.Read())
                {
                    string resourceItem = (reader.IsDBNull(0) ? string.Empty : reader["ResourceItem"].ToString());
                    int count = (reader.IsDBNull(1) ? 0 : (int)reader["Count"]);

                    if (!resourceItem.Equals(string.Empty) || !count.Equals(0))
                    {
                        craftItem.AddItemInList(resourceItem, count);
                    }
                }
                //Debug.Log("Read data completely");
            }
            if (!reader.IsClosed) reader.Close();

        }
        catch (Exception e)
        {
            //Debug.Log(e.Message);
            if (!reader.IsClosed) reader.Close();
            //Debug.Log("Connect failed");
        }
    }
}


