using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingInfo
{
    public Item item;
    public float thirst;
    public float hungry;

    public FishingInfo(Item item, float thirst, float hungry)
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
    public Item MackerelRaw;
    public float MackerelRawT;
    public float MackerelRawH;

    [Header("MackerelCooked")]
    public Item MackerelCooked;
    public float MackerelCookedT;
    public float MackerelCookedH;

    [Header("TilapiaRaw")]
    public Item TilapiaRaw;
    public float TilapiaRawT;
    public float TilapiaRawH;

    [Header("TilapiaCooked")]
    public Item TilapiaCooked;
    public float TilapiaCookedT;
    public float TilapiaCookedH;
}
