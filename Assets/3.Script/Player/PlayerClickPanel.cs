using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerClickPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public bool isPossibleClick = true;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPossibleClick = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPossibleClick = false;
    }
}