using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Craft2Manager : MonoBehaviour
{
    [SerializeField] GameObject[] slots;
    [SerializeField] RectTransform imagePanel;
    public Item[] selectedCategoryItems;

    private void SetCraft2ItemSlotInactive()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetActive(false);
        }
    }

    public void SetCraft2Panel(Item[] items)
    {
        //ItemSlotCraft2 set active as items's length
        SetCraft2ItemSlotInactive();

        //View Item list
        for (int j = 0; j < items.Length; j++)
        {
            slots[j].SetActive(true);
            slots[j].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = items[j].icon;
            slots[j].transform.GetChild(2).GetComponent<Text>().text = items[j].itemKoreanName;
        }

        //Modify Craft2Panel Size 
        imagePanel.offsetMin = new Vector2(0, 380 - 70 * (items.Length - 1));
    }
}
