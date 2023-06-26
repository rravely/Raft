using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) //���콺�� ������ ��
    {
        if (transform.childCount == 0) //�ڽ� ��ü�� ������(�������� ������)
        {
            SlotItem slotItem = eventData.pointerDrag.GetComponent<SlotItem>();
            slotItem.parentAfterDrag = transform;
            slotItem.transform.position = slotItem.parentAfterDrag.transform.position;
        }
        else //�̹� �������� �����ϸ� swap
        {
            SlotItem slotItem = eventData.pointerDrag.GetComponent<SlotItem>();

            transform.GetChild(0).position = slotItem.currentParent.transform.position;
            transform.GetChild(0).SetParent(slotItem.currentParent);

            slotItem.parentAfterDrag = transform;
            slotItem.transform.position = slotItem.parentAfterDrag.transform.position;
        }
    }
}