using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAimSlider : MonoBehaviour
{
    PlayerInteraction playerInteraction;
    PlayerClickPanel playerClickPanel;
    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        playerInteraction = FindAnyObjectByType<PlayerInteraction>();
        playerClickPanel = GetComponentInParent<PlayerClickPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        ChargingAim();
    }

    void ChargingAim()
    {
        if (playerClickPanel.isPossibleClick)
        {
            slider.value = playerInteraction.charging * 0.01f;
        }
    }
}
