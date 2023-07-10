using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEat : MonoBehaviour
{
    SelectedItem selectedItem;
    PlayerState playerState;
    PlayerInput playerInput;

    ItemManager itemManager;

    [SerializeField] GameObject LMBUI;

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
            //Show interaction ui
            LMBUI.SetActive(true);
            LMBUI.transform.GetChild(1).GetComponent<Text>().text = "¸Ô±â";

            if (playerInput.isLMDDown)
            {
                //Remove item in quickslot
                itemManager.RemoveItem(selectedItem.selectedItem, 1);

                PlayerAudio.instance.Chew();

                //Change player state
                playerState.moisture += FishItemDatabase.instance.ReturnThirst(selectedItem.selectedItem);
                playerState.satiation += FishItemDatabase.instance.ReturnHungry(selectedItem.selectedItem);

                LMBUI.SetActive(false);
            }
        }
    }
}
