using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;
    public Image image;
    public Text countText;
    public Slider toolSlider;
    public Transform canvas;

    //for click, drag events in inventory
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Transform currentParent;
    [HideInInspector] public int count = 1;


    void Awake()
    {
        canvas = GameObject.FindWithTag("Canvas").transform;

        image = GetComponentInChildren<Image>();
        countText = GetComponentInChildren<Text>();
        toolSlider = GetComponentInChildren<Slider>();
    }

    void Update()
    {
        if (count.Equals(0))
        { 
            Destroy(gameObject);
        }
    }


    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.icon;
        //if (newItem.isTool) toolSlider.gameObject.SetActive(true);
        RefreshCount();
    }


    public void RefreshCount()
    { 
        countText.text = count.ToString();
        if (count > 1)
        {
            countText.gameObject.SetActive(true);
        }
        else
        {
            countText.gameObject.SetActive(false);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        currentParent = transform.parent; 
        transform.SetParent(transform.root); 
        transform.SetAsLastSibling(); 
        image.raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.GetChild(0).GetComponent<Text>().enabled = false;
        transform.GetChild(1).gameObject.SetActive(false);
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.GetChild(0).GetComponent<Text>().enabled = true;
        CheckStackableAndSetSlider();
        transform.SetParent(parentAfterDrag);
        transform.position = parentAfterDrag.position;
        image.raycastTarget = true;
    }

    void CheckStackableAndSetSlider()
    {
        if (item.isTool)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }

}