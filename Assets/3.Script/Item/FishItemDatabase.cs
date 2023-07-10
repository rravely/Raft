using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishInfo
{
    public Item item;
    public float thirst;
    public float hungry;

    public FishInfo(Item item, float thirst, float hungry)
    {
        this.item = item;
        this.thirst = thirst;
        this.hungry = hungry;
    }
}

public class FishItemDatabase : MonoBehaviour
{
    public static FishItemDatabase instance = null;

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

    [Header("HerringRaw")]
    public Item herringRaw;
    public float herringRawT;
    public float herringRawH;

    [Header("HerringCooked")]
    public Item herringCooked;
    public float herringCookedT;
    public float herringCookedH;

    [Header("PomfretRaw")]
    public Item pomfretRaw;
    public float pomfretRawT;
    public float pomfretRawH;

    [Header("PomfretCooked")]
    public Item pomfretCooked;
    public float pomfretCookedT;
    public float pomfretCookedH;

    [Header("MackerelRaw")]
    public Item mackerelRaw;
    public float mackerelRawT;
    public float mackerelRawH;

    [Header("MackerelCooked")]
    public Item mackerelCooked;
    public float mackerelCookedT;
    public float mackerelCookedH;

    [Header("TilapiaRaw")]
    public Item tilapiaRaw;
    public float tilapiaRawT;
    public float tilapiaRawH;

    [Header("TilapiaCooked")]
    public Item tilapiaCooked;
    public float tilapiaCookedT;
    public float tilapiaCookedH;

    public FishInfo[] fishes = new FishInfo[8];

    private void Start()
    {
        fishes[0] = new FishInfo(herringRaw, herringRawT, herringRawH);
        fishes[1] = new FishInfo(herringCooked, herringCookedT, herringCookedH);
        fishes[2] = new FishInfo(pomfretRaw, pomfretRawT, pomfretRawH);
        fishes[3] = new FishInfo(pomfretCooked, pomfretCookedT, pomfretCookedH);
        fishes[4] = new FishInfo(mackerelRaw, mackerelRawT, mackerelCookedH);
        fishes[5] = new FishInfo(mackerelCooked, mackerelCookedT, mackerelCookedH);
        fishes[6] = new FishInfo(tilapiaRaw, tilapiaRawT, tilapiaRawH);
        fishes[7] = new FishInfo(tilapiaCooked, tilapiaCookedT, tilapiaCookedH);
    }

    public float ReturnThirst(Item item)
    {
        for (int i = 0; i < fishes.Length; i++)
        {
            if (fishes[i].item.Equals(item))
            {
                return fishes[i].thirst;
            }
        }

        return 0f;
    }

    public float ReturnHungry(Item item)
    {
        for (int i = 0; i < fishes.Length; i++)
        {
            if (fishes[i].item.Equals(item))
            {
                return fishes[i].hungry;
            }
        }

        return 0f;
    }

    public Item ReturnCookedFish(Item rawFish)
    {
        switch (rawFish.itemName)
        {
            case "HerringRaw":
                return herringCooked;
            case "PomfretRaw":
                return pomfretCooked;
            case "MackerelRaw":
                return mackerelCooked;
            case "TilapiaRaw":
                return tilapiaCooked;
            default:
                return null;
        }
    }
}
