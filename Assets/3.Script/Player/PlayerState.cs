using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [HideInInspector] public float moisture;
    [HideInInspector] public float satiation;
    [HideInInspector] public float health;

    
    public const float maxMoisture = 100f;
    public const float maxSatiation = 100f;
    public const float maxHealth = 100f;

    public Dictionary<string, int> playerItems = new Dictionary<string, int>();

    [HideInInspector] public bool isJump = false;
    [HideInInspector] public bool inWater = false;
    [HideInInspector] public bool inWaterSurface = false;

    public void InitializePlayerState()
    {
        moisture = maxMoisture;
        satiation = maxSatiation;
        health = maxHealth;
    }

    public void AddPlayerItems(Item item)
    {
        if (playerItems.ContainsKey(item.itemName))
        {
            playerItems[item.itemName]++;
        }
        else
        {
            playerItems.Add(item.itemName, 1);
        }
    }

    public void RemovePlayerItems(Item item, int count)
    {
        if (playerItems.ContainsKey(item.itemName))
        {
            playerItems[item.itemName] -= count;
        }
    }

    public int FindItemCount(Item item)
    {
        if (playerItems.ContainsKey(item.itemName))
        {
            return playerItems[item.itemName];
        }
        else
        {
            return 0;
        }
    }
}
