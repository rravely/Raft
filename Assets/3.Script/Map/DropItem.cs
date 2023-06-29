using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropItem : MonoBehaviour
{
    [Header("Drop Item")]
    [SerializeField] public Item dropItem;

    [Header("Fake Hook")]
    [SerializeField] Transform fakeHook;

    GameObject ui;
    PlayerInteraction playerInteraction;

    float speed = 0.1f;
    [HideInInspector] public bool isHooked = false;

    string playerTag = "PlayerSight";

    private void Start()
    {
        ui = GameObject.FindWithTag("InteractionUI");
        playerInteraction = FindObjectOfType<PlayerInteraction>();
    }

    private void Update()
    {
        if (!isHooked)
        {
            transform.position -= Vector3.forward * speed * Time.deltaTime;
        }
        else
        {
            transform.position = new Vector3(fakeHook.transform.position.x + 0.1f, fakeHook.transform.position.y, fakeHook.transform.position.z);
        }
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

        ui.transform.GetChild(1).GetComponent<Text>().text = string.Format($"{dropItem.itemKoreanName} ащ╠Б");
    }    
}
