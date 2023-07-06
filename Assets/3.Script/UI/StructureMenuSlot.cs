using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StructureMenuSlot : MonoBehaviour, IPointerEnterHandler
{
    [Header("Structure Manager")]
    [SerializeField] StructureManager structureManager;

    [Header("Item")]
    [SerializeField] Item structureItem;

    [Header("Title image")]
    [SerializeField] Image titleImage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        titleImage.sprite = transform.GetChild(0).GetComponent<Image>().sprite;
        structureManager.selectedStructureItem = structureItem;
    }
}
