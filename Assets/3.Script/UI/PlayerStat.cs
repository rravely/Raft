using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    PlayerState playerState;

    [SerializeField] Slider thirst;
    [SerializeField] Slider hunger;
    [SerializeField] Slider health;

    // Start is called before the first frame update
    void Start()
    {
        playerState = FindObjectOfType<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        thirst.value = playerState.moisture * 0.01f;
        hunger.value = playerState.satiation * 0.01f;
        health.value = playerState.health * 0.01f;
    }
}
