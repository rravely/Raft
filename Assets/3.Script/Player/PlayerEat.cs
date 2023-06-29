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

    // Update is called once per frame
    void Update()
    {
        switch (selectedItem.selectedItem.itemName)
        {
            case "CupSaltWater":
                SetActiveUI(true, "마시기");
                if (playerInput.isLMDDown)
                {
                    playerState.moisture -= 10;
                    itemManager.RemoveItem(selectedItem.selectedItem, 1);
                }
                break;
            case "CupFreshWater":
                SetActiveUI(true, "마시기");
                if (playerInput.isLMDDown)
                {
                    playerState.moisture += 10;
                    itemManager.RemoveItem(selectedItem.selectedItem, 1);
                }
                break;
            default:
                SetActiveUI(false, "");
                break;
        }
    }

    void SetActiveUI(bool isActive, string text)
    {
        LMBUI.SetActive(isActive);
        LMBUI.transform.GetChild(1).GetComponent<Text>().text = text;
    }
}
