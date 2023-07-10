using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsRod : MonoBehaviour
{
    [Header("Fishes")]
    [SerializeField] Item[] fishes;

    [Header("Audio Clip")]
    AudioSource handsAudio;
    [SerializeField] AudioClip[] audioClips;

    ItemManager itemManager;

    int randomIndex;

    private void OnEnable()
    {
        itemManager = FindObjectOfType<ItemManager>();
        handsAudio = GetComponent<AudioSource>();
    }

    public void AcquiredRandomFish()
    {
        randomIndex = Random.Range(0, fishes.Length);

        itemManager.AddItem(fishes[randomIndex]);
    }

    public void PlayFishOnHookSound()
    {
        handsAudio.Play();
        handsAudio.loop = true;
    }

    public void StopFishOnHookSound()
    {
        handsAudio.Stop();
        handsAudio.loop = false;
    }
}
