using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grill : MonoBehaviour
{
    Transform interactionUI;

    AudioSource audioGrill;

    bool isEmpty = true; //Is there any fish on the grill?
    bool isPlankExist = false;
    bool isCooked = false;
    bool canInteraction = false; //Is there a player near the grill?

    Item fishRaw;
    Item fishCooked;
    int childIndex = 0;

    PlayerInput playerInput;
    SelectedItem selectedItem;

    ItemManager itemManager;

    private void Start()
    {
        interactionUI = GameObject.FindWithTag("InteractionUI").transform;

        audioGrill = GetComponent<AudioSource>();

        playerInput = FindObjectOfType<PlayerInput>();
        selectedItem = FindObjectOfType<SelectedItem>();
        itemManager = FindObjectOfType<ItemManager>();
    }

    private void Update()
    {
        if (isEmpty && !isPlankExist && !isCooked && canInteraction)
        {
            if (selectedItem.selectedItem.isFish && !selectedItem.selectedItem.isCooked)
            {
                //Show selected item name and ask interaction by ui
                ActivateInteractionUI(true, string.Format($"{selectedItem.selectedItem.itemKoreanName} 놓기"));

                if (Input.GetKeyDown(KeyCode.E))
                {
                    //Set fish on grill
                    //Turn on fish object
                    childIndex = SetRawFishOnGrill(selectedItem.selectedItem);
                    fishRaw = selectedItem.selectedItem;
                    fishCooked = FishItemDatabase.instance.ReturnCookedFish(fishRaw);

                    isEmpty = false;

                    //Remove item in quickslot
                    itemManager.RemoveItemInSlot(fishRaw, 1, selectedItem.selectedButtonIndex);
                }
            }
        }
        else if (!isEmpty && !isPlankExist && !isCooked && canInteraction)
        {
            //Show interaction ui with asking plank 
            ActivateInteractionUI(true, "판자 놓기");

            if (Input.GetKeyDown(KeyCode.E))
            {
                //Set plank
                isPlankExist = true;
                ActivatePlank(isPlankExist);

                //Start Fire
                transform.GetChild(2).gameObject.SetActive(true);
                audioGrill.Play();

                //Inactive interactionUI
                ActivateInteractionUI(false, "");

                //count down 
                StartCoroutine(CookFish_co(10f));
            }
        }
        else if (!isEmpty && !isPlankExist && isCooked && canInteraction)
        {
            //Change UI
            ActivateInteractionUI(true, string.Format($"{fishCooked.itemKoreanName} 선택"));

            if (Input.GetKeyDown(KeyCode.E))
            {
                //Add Item 
                itemManager.AddItem(fishCooked);

                //Inactive cooked fish object
                transform.GetChild(0).GetChild(0).GetChild(childIndex).gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteraction = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteraction = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteraction = false;
        }
    }

    int SetRawFishOnGrill(Item item)
    {
        int fishIndex = 0;
        switch (item.itemName)
        {
            case "HerringRaw":
                transform.GetChild(0).GetChild(1).GetChild(3).gameObject.SetActive(true);
                fishIndex = 3;
                break;

            case "PomfretRaw":
                transform.GetChild(0).GetChild(1).GetChild(5).gameObject.SetActive(true);
                fishIndex = 5;
                break;

            case "MackerelRaw":
                transform.GetChild(0).GetChild(1).GetChild(4).gameObject.SetActive(true);
                fishIndex = 4;
                break;

            case "TilapiaRaw":
                transform.GetChild(0).GetChild(1).GetChild(8).gameObject.SetActive(true);
                fishIndex = 8;
                break;
            default:
                fishIndex = 0;
                break;
        }
        return fishIndex;
    }

    IEnumerator CookFish_co(float cookingTime)
    {
        float time = 0f;
        while (time < cookingTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        //Stop fire audio
        audioGrill.Stop();

        //Inactivate plank
        isPlankExist = false;
        ActivatePlank(isPlankExist);

        //Inactivate fire
        transform.GetChild(2).gameObject.SetActive(false);

        //Change raw fish to cooked fish
        isCooked = true;
        ChangeFishObject(childIndex);

    }

    void ActivatePlank(bool isActive)
    {
        transform.GetChild(1).GetChild(0).gameObject.SetActive(isActive);
        transform.GetChild(1).GetChild(1).gameObject.SetActive(isActive);
    }

    void ChangeFishObject(int index)
    {
        transform.GetChild(0).GetChild(1).GetChild(index).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(0).GetChild(index).gameObject.SetActive(true);
    }

    void ActivateInteractionUI(bool isActive, string text)
    {
        interactionUI.GetChild(0).gameObject.SetActive(isActive);
        interactionUI.GetChild(1).gameObject.SetActive(isActive);
        interactionUI.GetChild(1).GetComponent<Text>().text = text;
    }
}
