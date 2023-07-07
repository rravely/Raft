using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEat : MonoBehaviour
{
    SelectedItem selectedItem;
    PlayerState playerState;
    PlayerInput playerInput;

    ItemManager itemManager;

    // Start is called before the first frame update
    void Start()
    {
        selectedItem = GetComponent<SelectedItem>();
        playerState = GetComponent<PlayerState>();
        playerInput = GetComponent<PlayerInput>();

        itemManager = FindObjectOfType<ItemManager>();
    }

    private void Update()
    {
        if (selectedItem.selectedItem.isFish)
        {
            if (playerInput.isLMDDown)
            {
                //Remove item in quickslot
                itemManager.RemoveItem(selectedItem.selectedItem, 1);

                //Change player state
                playerState.moisture += FishItemDatabase.instance.ReturnThirst(selectedItem.selectedItem);
                playerState.satiation += FishItemDatabase.instance.ReturnHungry(selectedItem.selectedItem);
            }
        }
    }
}
