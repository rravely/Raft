using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StructureMenuSlot : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] Item structureItem;
    [SerializeField] Image titleImage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        titleImage.sprite = transform.GetChild(0).GetComponent<Image>().sprite;
        //titleImage.rectTransform = transform.GetChild(0).GetComponent<Image>().rectTransform;
    }
}
