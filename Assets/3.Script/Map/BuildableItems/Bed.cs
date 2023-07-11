using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bed : MonoBehaviour
{
    bool canSleep = false;
    bool isLie = false;

    PlayerSleep playerSleep;
    PlayerState playerState;

    Transform interactionUI;

    private void Start()
    {
        playerSleep = FindObjectOfType<PlayerSleep>();
        playerState = FindObjectOfType<PlayerState>();
        interactionUI = GameObject.FindWithTag("InteractionUI").transform;
    }

    private void Update()
    {
        if (canSleep && !isLie)
        {
            //ui
            ActivateInteractionUI(true, "눕기");
            
            if (Input.GetKeyDown(KeyCode.E)) //Lie on bed
            {
                playerSleep.PlayerLieOnBed(transform.GetChild(0).position);
                isLie = true;
            }
        }
        else if (canSleep && isLie)
        {
            //ui
            ActivateInteractionUI(true, "일어나기");

            if (Input.GetKeyDown(KeyCode.E))
            {
                playerSleep.PlayerGetUp(transform.GetChild(1).position);
                playerState.isSleep = false;
                isLie = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canSleep = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canSleep = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canSleep = false;
            ActivateInteractionUI(false, "");
        }
    }

    void ActivateInteractionUI(bool isActive, string text)
    {
        interactionUI.GetChild(0).gameObject.SetActive(isActive);
        interactionUI.GetChild(1).gameObject.SetActive(isActive);
        interactionUI.GetChild(1).GetComponent<Text>().text = text;
    }
}
