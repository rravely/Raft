using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] Item dropItem;
    GameObject ui;

    PlayerInteraction playerInteraction;

    float speed = 0.1f;

    string playerTag = "PlayerSight";

    private void Start()
    {
        ui = GameObject.FindWithTag("InteractionUI");
        playerInteraction = FindObjectOfType<PlayerInteraction>();
    }

    private void Update()
    {
        transform.position -= Vector3.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            //Display e button
            SetActiveUI(true);

            playerInteraction.canPickup = true;
            playerInteraction.pickupItem = dropItem;
            playerInteraction.pickupObject = gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            SetActiveUI(false);

            playerInteraction.canPickup = false;
            playerInteraction.pickupItem = null;
            playerInteraction.pickupObject = null;
        }
    }

    void SetActiveUI(bool isActive)
    {
        ui.transform.GetChild(0).gameObject.SetActive(isActive);
        ui.transform.GetChild(1).gameObject.SetActive(isActive);
    }    
}
