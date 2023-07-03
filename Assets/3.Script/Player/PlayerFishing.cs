using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFishing : MonoBehaviour
{
    PlayerInput playerInput;
    SelectedItem selectedItem;

    Animator fishingHandsAni;

    bool isFishing = false;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        selectedItem = GetComponent<SelectedItem>();

        fishingHandsAni = GetComponent<Animator>();
    }

    private void Update()
    {
        if (selectedItem.selectedItem.itemName.Equals("FishingRod") && playerInput.isLMDDown && !isFishing) 
        {
            fishingHandsAni.SetBool("RodDrop", true);
        }
    }
}
