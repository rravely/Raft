using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftSlot : MonoBehaviour, IPointerEnterHandler
{
    [Header("Craft2")]
    [SerializeField] Image craft2Image;
    [SerializeField] Craft2Manager craft2Manager;

    [Header("Title Name")]
    [SerializeField] Text craft2TitleName;
    [SerializeField] string titleName;

    [Header("Items")]
    [SerializeField] Item[] items;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetSelectedButton();
    }

    void SetSelectedButton()
    {
        //Set Image and title name
        craft2Image.sprite = transform.GetChild(0).GetComponent<Image>().sprite;
        craft2TitleName.text = titleName;

        //Change Panel size
        craft2Manager.SetCraft2Panel(items);

        //Give manager information of items
        craft2Manager.selectedCategoryItems = items;
    }
}
