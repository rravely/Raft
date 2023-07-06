using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo
{
    public Item item;
    public Item[] resourcesItems;
    public int[] resourcesItemCount;

    public ItemInfo(Item item, Item[] resourcesItems, int[] resourcesItemCount)
    {
        this.item = item;
        this.resourcesItems = resourcesItems;
        this.resourcesItemCount = resourcesItemCount;
    }
}

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance = null;

    void Awake()
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
    }

#region Using Dictionary

    public Dictionary<string, int>[][] itemDB = new Dictionary<string, int>[4][];

    public Dictionary<string, int>[] FoodWater = new Dictionary<string, int>[3]
    {
        new Dictionary<string, int>() {{"Plastic", 4 }},
        new Dictionary<string, int>() {{"Plank", 6}},
        new Dictionary<string, int>() {{"Plank", 6}, {"Thatch", 6}, {"Plastic", 4}}
    };

    public Dictionary<string, int>[] Other = new Dictionary<string, int>[]
    {
        new Dictionary<string, int>() {{"Thatch", 10}, {"Plank", 6}, {"Plastic", 4}}
    };

    public Dictionary<string, int>[] Tools = new Dictionary<string, int>[2]
    {
        new Dictionary<string, int>() {{"Plank", 4}},
        new Dictionary<string, int>() {{"Plank", 1}, {"Plastic", 2}}
    };

    public Dictionary<string, int>[] Weapons = new Dictionary<string, int>[1]
    {
        new Dictionary<string, int>() {{"Plank", 8}}
    };
    #endregion

    
    [Header("Cup Empty")]
    public Item cupEmpty;
    public Item[] cupEmptyR;
    public int[] cupEmptyI;

    [Header("Grill Small")]
    public Item grillSmall;
    public Item[] grillSmallR;
    public int[] grillSmallI;

    [Header("Purifier")]
    public Item purifier;
    public Item[] purifierR;
    public int[] purifierI;

    [Header("Bed Simple")]
    public Item bedSimple;
    public Item[] bedSimpleR;
    public int[] bedSimpleI;

    [Header("Hammer")]
    public Item hammer;
    public Item[] hammerR;
    public int[] hammerI;

    [Header("Plastic Hook")]
    public Item plasticHook;
    public Item[] plasticHookR;
    public int[] plasticHookI;

    [Header("WoodSpear")]
    public Item woodSpear;
    public Item[] woodSpearR;
    public int[] woodSpearI;

    [Header("Rope")]
    public Item rope;
    public Item[] ropeR;
    public int[] ropeI;

    [Header("Nail")]
    public Item nail;
    public Item[] nailR;
    public int[] nailI;

    [Header("Sail")]
    public Item sail;
    public Item[] sailR;
    public int[] sailI;

    [Header("Chair")]
    public Item chair;
    public Item[] chairR;
    public int[] chairI;

    [Header("Table")]
    public Item table;
    public Item[] tableR;
    public int[] tableI;


    public ItemInfo[] items = new ItemInfo[13];

    void Start()
    {
        items[0] = new ItemInfo(cupEmpty, cupEmptyR, cupEmptyI);
        items[1] = new ItemInfo(grillSmall, grillSmallR, grillSmallI);
        items[2] = new ItemInfo(purifier, purifierR, purifierI);
        items[3] = new ItemInfo(bedSimple, bedSimpleR, bedSimpleI);
        items[4] = new ItemInfo(hammer, hammerR, hammerI);
        items[5] = new ItemInfo(plasticHook, plasticHookR, plasticHookI);
        items[6] = new ItemInfo(woodSpear, woodSpearR, woodSpearI);
        items[7] = new ItemInfo(rope, ropeR, ropeI);
        items[8] = new ItemInfo(nail, nailR, nailI);
        items[9] = new ItemInfo(sail, sailR, sailI);
        items[10] = new ItemInfo(chair, chairR, chairI);
        items[11] = new ItemInfo(table, tableR, tableI);
   }

    public int FindIndexOfDB(Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].item.itemName.Equals(item.itemName))
            {
                return i;
            }
        }
        return 10000;
    }
}
