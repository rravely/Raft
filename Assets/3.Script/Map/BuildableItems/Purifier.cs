using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Purifier : MonoBehaviour
{
    SelectedItem selectedItem;
    ItemManager itemManager;
    PlayerInteraction playerInteraction;
    BuildableItem buildableItem;

    [Header("Cup items")]
    [SerializeField] Item cupEmpty;
    [SerializeField] Item cupFreshWater;

    [Header("Fire audio clip")]
    AudioSource audio;
    AudioClip fire;

    Transform interactionUI;

    bool isEmpty = true;
    bool isFreshWater = false;

    private void Start()
    {
        selectedItem = FindObjectOfType<SelectedItem>();
        itemManager = FindObjectOfType<ItemManager>();
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        buildableItem = GetComponent<BuildableItem>();
        audio = GetComponent<AudioSource>();

        interactionUI = GameObject.FindWithTag("InteractionUI").transform;
    }

    private void Update()
    {
        if (buildableItem.isBuilt)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && selectedItem.selectedItem.itemName.Equals("CupSaltWater") && isEmpty)
        {
            //view [E]
            ActivateInteractionUI(true, "바닷물 컵 놓기");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && selectedItem.selectedItem.itemName.Equals("CupSaltWater") && isEmpty && !isFreshWater)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Fill water in purifier
                isEmpty = false;
                transform.GetChild(2).gameObject.SetActive(true);

                //Play player animation
                playerInteraction.InteractionHands();

                //Change cupsaltwater to cupEmpty
                itemManager.ChangeItem(selectedItem.selectedItem, cupEmpty, selectedItem.selectedButtonIndex);

                //Fire audio
                audio.Play();

                //Inactive UI
                ActivateInteractionUI(false, "");

                //Start purifier
                StartCoroutine(PurifierWater_co());
            }
        }
        else if (other.CompareTag("Player") && selectedItem.selectedItem.itemName.Equals("CupEmpty") && !isEmpty && isFreshWater)
        {
            ActivateInteractionUI(true, "담수 채우기");

            if (Input.GetKeyDown(KeyCode.E))
            {
                //Empty purifier
                isEmpty = true;
                transform.GetChild(2).gameObject.SetActive(false);

                //Play player animation
                playerInteraction.InteractionHands();

                //Change cupEmpty to cupFreshWater
                itemManager.ChangeItem(selectedItem.selectedItem, cupFreshWater, selectedItem.selectedButtonIndex);

                //Inactive UI
                ActivateInteractionUI(false, "");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ActivateInteractionUI(false, "");
    }

    IEnumerator PurifierWater_co()
    {
        float time = 0f;
        float maxTime = 5f;

        transform.GetChild(3).gameObject.SetActive(true);

        while (time < maxTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        isFreshWater = true;
        audio.Stop();
        transform.GetChild(3).gameObject.SetActive(false);
    }

    void ActivateInteractionUI(bool isActive, string text)
    {
        interactionUI.GetChild(0).gameObject.SetActive(isActive);
        interactionUI.GetChild(1).gameObject.SetActive(isActive);
        interactionUI.GetChild(1).GetComponent<Text>().text = text;
    }

}
