using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSight : MonoBehaviour
{
    SelectedItem selectedItem;
    ItemManager itemManager;
    PlayerInteraction playerInteraction;

    [SerializeField] Item cupSaltWater;

    Transform interactionUI;

    private void Start()
    {
        selectedItem = FindObjectOfType<SelectedItem>();
        itemManager = FindObjectOfType<ItemManager>();
        playerInteraction = FindObjectOfType<PlayerInteraction>();

        interactionUI = GameObject.FindWithTag("InteractionUI").transform;
    }

    private void Update()
    {
        if (Physics.Raycast(/*Camera.main.ScreenPointToRay(new Vector3(960, 540))*/ transform.position, transform.forward, out RaycastHit hit, 0.7f))
        {
            if (selectedItem.selectedItem.itemName.Equals("CupEmpty") && hit.transform.tag.Equals("Water"))
            {
                //view [E] ¹° ¶ß±â
                ActivateInteractionUI(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    itemManager.ChangeItem(selectedItem.selectedItem, cupSaltWater, selectedItem.selectedButtonIndex);
                    selectedItem.selectedItem = cupSaltWater;

                    //Play player animation
                    playerInteraction.InteractionHands();

                    ActivateInteractionUI(false);
                }
            }
            else if (!hit.transform.TryGetComponent<DropItem>(out DropItem dropItem))
            {

            }
            else
            {
                ActivateInteractionUI(false);
            }
        }
    }

    void ActivateInteractionUI(bool isActive)
    {
        interactionUI.GetChild(0).gameObject.SetActive(isActive);
        interactionUI.GetChild(1).gameObject.SetActive(isActive);
        interactionUI.GetChild(1).GetComponent<Text>().text = "¹Ù´å¹° Ã¤¿ì±â"; 
    }    
}
