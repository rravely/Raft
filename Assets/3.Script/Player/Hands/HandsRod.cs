using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsRod : MonoBehaviour
{
    [Header("Fishes")]
    [SerializeField] Item[] fishes;

    ItemManager itemManager;

    int randomIndex;

    private void OnEnable()
    {
        itemManager = FindObjectOfType<ItemManager>();
    }

    public void AcquiredRandomFish()
    {
        randomIndex = Random.Range(0, fishes.Length);

        itemManager.AddItem(fishes[randomIndex]);
    }
}
