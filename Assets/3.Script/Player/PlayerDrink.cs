using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDrink : MonoBehaviour
{
    SelectedItem selectedItem;
    PlayerState playerState;
    PlayerInput playerInput;

    ItemManager itemManager;

    [SerializeField] Item cupEmpty;
    [SerializeField] Item cupSaltWater;
    [SerializeField] Item cupFreshWater;

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
                    PlayerAudio.instance.Drink();
                    //itemManager.RemoveItem(selectedItem.selectedItem, 1);
                    itemManager.ChangeItem(cupSaltWater, cupEmpty, selectedItem.selectedButtonIndex);
                }
                break;
            case "CupFreshWater":
                SetActiveUI(true, "마시기");
                if (playerInput.isLMDDown)
                {
                    playerState.moisture += 10;
                    PlayerAudio.instance.Drink();
                    //itemManager.RemoveItem(selectedItem.selectedItem, 1);
                    itemManager.ChangeItem(cupFreshWater, cupEmpty, selectedItem.selectedButtonIndex);
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
