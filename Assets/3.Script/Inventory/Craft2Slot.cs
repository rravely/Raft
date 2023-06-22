using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Craft2Slot : MonoBehaviour, IPointerEnterHandler
{
    [Header("Craft3 Image")]
    [SerializeField] Image craft3Image;
    [SerializeField] Craft2Manager craft2Manager;
    [SerializeField] Craft3Manager craft3Manager;

    [Header("Slot Index")]
    public int index;


    public void OnPointerEnter(PointerEventData eventData)
    {
        SetSelectedButton();
    }

    void SetSelectedButton()
    {
        //Set title image
        craft3Image.sprite = transform.GetChild(0).GetComponent<Image>().sprite;

        craft3Manager.selectedItem = craft2Manager.selectedCategoryItems[index];
        craft3Manager.SetCraft3Panel();
        //craft3Manager.selectedItemIndex = index;
    }
}
