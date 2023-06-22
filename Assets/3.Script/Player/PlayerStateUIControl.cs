using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateUIControl : MonoBehaviour
{
    PlayerState playerState;

    [Header("State Bar")]
    [SerializeField] Slider moistureBar;
    [SerializeField] Slider satiationBar;
    [SerializeField] Slider healthBar;


    // Start is called before the first frame update
    void Start()
    {
        playerState = GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisplayPlayerState()
    {
        
    }
}
