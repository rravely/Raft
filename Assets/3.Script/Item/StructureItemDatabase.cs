using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureItemDatabase : MonoBehaviour
{
    static public StructureItemDatabase instance = null;

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
    }

    [Header("Foundation")]
    public Item foundation;
    public Item[] foundationR;
    public int[] foundationI;

    [Header("Pillar")]
    public Item pillar;
    public Item[] pillarR;
    public int[] pillarI;

    [Header("Floor")]
    public Item floor;
    public Item[] floorR;
    public int[] floorI;

    [Header("Stairs")]
    public Item staris;
    public Item[] stairsR;
    public int[] starisI;

    [Header("Wall Wood")]
    public Item wallWood;
    public Item[] wallWoodR;
    public int[] wallWoodI;

    [Header("Half Wall Wood")]
    public Item halfWallWood;
    public Item[] halfWallWoodR;
    public int[] halfWallWoodI;

    public ItemInfo[] structureItems = new ItemInfo[6];

    private void Start()
    {
        structureItems[0] = new ItemInfo(foundation, foundationR, foundationI);
        structureItems[1] = new ItemInfo(pillar, pillarR, pillarI);
        structureItems[2] = new ItemInfo(floor, floorR, floorI);
        structureItems[3] = new ItemInfo(staris, stairsR, starisI);
        structureItems[4] = new ItemInfo(wallWood, wallWoodR, wallWoodI);
        structureItems[5] = new ItemInfo(halfWallWood, halfWallWoodR, halfWallWoodI);
    }

    public int FindIndexOfDB(Item item)
    {
        for (int i = 0; i < structureItems.Length; i++)
        {
            if (structureItems[i].item.itemName.Equals(item.itemName))
            {
                return i;
            }
        }
        return 10000;
    }
}
