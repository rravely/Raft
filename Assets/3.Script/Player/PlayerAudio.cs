using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    AudioSource[] audio;

    [Header("Player Sound")]
    [SerializeField] AudioClip playerDamage;
    [SerializeField] AudioClip fatigue;
    [SerializeField] AudioClip hungry;
    [SerializeField] AudioClip thirsty;

    [Header("One shot Sound")]
    [SerializeField] AudioClip drink;
    [SerializeField] AudioClip pickupWater;
    [SerializeField] AudioClip throwObject;
    [SerializeField] AudioClip placeObject;
    [SerializeField] AudioClip waterPlump;

    static public PlayerAudio instance = null;

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

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponents<AudioSource>();
    }

    public void PlayerDamaged()
    {
        audio[0].PlayOneShot(playerDamage);
    }

    public void PlayerStat(int index)
    {
        switch (index)
        {
            case 0:
                audio[0].clip = thirsty;
                break;
            case 1:
                audio[0].clip = hungry;
                break;
            case 2:
                audio[0].clip = fatigue;
                break;
        }
    }

    public void Drink()
    {
        audio[1].PlayOneShot(drink);
    }

    public void PickupWater()
    {
        audio[1].PlayOneShot(pickupWater);
    }

    public void Throw()
    {
        audio[1].PlayOneShot(throwObject);
    }

    public void PlaceObject()
    {
        audio[1].PlayOneShot(placeObject);
    }

    public void WaterPlump()
    {
        audio[1].PlayOneShot(waterPlump);
    }
}
