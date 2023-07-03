using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryInput : MonoBehaviour
{
    [Header("Quick Slot Buttons")]
    public GameObject[] quickSlotBtns;
    public int selectedButton = 0;
    SelectedItem selectedItem;

    [Header("Null Item")]
    [SerializeField] Item nullItem;

    [Header("Inventory")]
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject craftPanel;
    [SerializeField] Transform quickSlotPanel;

    [Header("Aim")]
    [SerializeField] GameObject aim;

    bool isInventoryOpen = false;
    bool isCraftOpen = false;

    private void Start()
    {
        selectedItem = GetComponent<SelectedItem>();
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(quickSlotBtns[0]);
    }

    // Update is called once per frame
    void Update()
    {
        InputQuickSlotKey();
        InputQuickSlotWheel();
        OpenInventory();
        OpenCraft();
    }

    private void InputQuickSlotKey()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedButton = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedButton = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedButton = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedButton = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectedButton = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            selectedButton = 5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            selectedButton = 6;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            selectedButton = 7;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            selectedButton = 8;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            selectedButton = 9;
        }
        EventSystem.current.SetSelectedGameObject(quickSlotBtns[selectedButton]);

        selectedItem.selectedItem = CheckSlotItem(selectedButton);
        selectedItem.selectedButtonIndex = selectedButton;
    }

    private void InputQuickSlotWheel()
    {
        if (Input.mouseScrollDelta.y < 0)
        {
            if (selectedButton.Equals(0))
            {
                selectedButton = 9;
            }
            else
            {
                selectedButton--; 
            }
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            if (selectedButton.Equals(9))
            {
                selectedButton = 0;
            }
            else
            {
                selectedButton++;
            }
        }
        EventSystem.current.SetSelectedGameObject(quickSlotBtns[selectedButton]);
    }

    private void OpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isInventoryOpen)
            {
                isInventoryOpen = true;
                inventoryPanel.SetActive(true);
                aim.SetActive(false);
            }
            else
            {
                isInventoryOpen = false;
                inventoryPanel.SetActive(false);
                aim.SetActive(true);
            }
        }
    }

    private void OpenCraft()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isCraftOpen)
            {
                isCraftOpen = true;
                inventoryPanel.SetActive(true);
                craftPanel.SetActive(true);
                aim.SetActive(false);
            }
            else
            {
                isCraftOpen = false;
                inventoryPanel.SetActive(false);
                craftPanel.SetActive(false);
                aim.SetActive(true);
            }
        }
    }

    public Item CheckSlotItem(int selectedButton)
    {
        if (quickSlotPanel.GetChild(selectedButton).childCount >= 1)
        {
            return quickSlotPanel.GetChild(selectedButton).GetComponentInChildren<SlotItem>().item;
        }
        else
        {
            return nullItem;
        }
    }

    public void DecreaseDurability(float count)
    {
        //quickSlotBtns[selectedButton].transform.GetChild(0).GetComponentInChildren<Slider>().value -= count;

        for (int i = 0; i < quickSlotBtns.Length; i++)
        {
            if (quickSlotBtns[i].transform.childCount > 0)
            {
                if (quickSlotBtns[i].transform.GetChild(0).GetComponent<SlotItem>().item.itemName.Equals("Hammer"))
                {
                    quickSlotBtns[i].transform.GetChild(0).GetComponentInChildren<Slider>().value -= count;
                    break;
                }
            }
        }
    }
}
