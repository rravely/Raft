using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    PlayerInput playerInput;
    InventoryInput inventoryInput;
    SelectedItem selectedItem;
    ItemManager itemManager;

    GameObject ui;
    
    [Header("Mouse Click Raycast Image")]
    [SerializeField] PlayerClickPanel playerClickPanel;

    [HideInInspector] public float charging = 0f;

    [Header("Animator")]
    [SerializeField] Animator playerAni;
    [SerializeField] Animator hammerAni;
    [SerializeField] Animator hookAni;

    [Header("Tools")]
    [SerializeField] GameObject hammer;

    [SerializeField] Transform hookT;

    public bool isHookThrown = false;

    public bool canPickup = false;
    public Item pickupItem = null;
    public GameObject pickupObject = null;

    public bool isLock = false;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        inventoryInput = GetComponent<InventoryInput>();
        selectedItem = GetComponent<SelectedItem>();
        itemManager = FindObjectOfType<ItemManager>();

        ui = GameObject.FindWithTag("InteractionUI");
    }

    private void Update()
    {
        Tools();
        Interaction();
    }

    void Tools()
    {
        switch (selectedItem.selectedItem.itemName)
        {
            case "Hammer":
                if (!isLock)
                {
                    playerAni.SetTrigger("Grab");
                    isLock = true;
                }
                Hammer();
                break;
            case "PlasticHook":
                if (!isLock)
                {
                    playerAni.SetTrigger("Grab");
                    isLock = true;
                }
                Hook();
                break;
            default:
                if (!selectedItem.selectedItem.isBuildable)
                {
                    isLock = false;
                }
                
                break;
        }
    }

    public void GrabHands()
    {
        playerAni.SetTrigger("Grab");
        isLock = true;
    }

    public void Hammer()
    {
        if (playerClickPanel.isPossibleClick)
        {
            if (playerInput.isLMDDown)
            {
                hammer.SetActive(true);

                playerAni.SetTrigger("Hammer");
                hammerAni.SetTrigger("Hammer");

                inventoryInput.DecreaseDurability(0.1f);
            }
        }
    }

    void Hook()
    {
        if (playerClickPanel.isPossibleClick)
        {
            if (playerInput.isLMD)
            {
                playerAni.SetBool("HookCharging", true);
                hookAni.SetTrigger("Charge");
                if (charging < 100)
                {
                    charging += 1f;
                }
            }
            else if (isHookThrown)
            {
                if (playerInput.isRMD)
                {
                    //Pull hook
                    Debug.Log("hook 당기는 중");
                    playerAni.SetBool("HookCharging", false);
                }
                else if (playerInput.isRMDDown)
                {
                    //Pull hook right away
                }
            }


        }

        if (playerInput.isLMDUp && charging > 5)
        {
            //Throw hook
            hookAni.SetTrigger("Throw");
            playerAni.SetTrigger("Throw");
            
            isHookThrown = true; //Hook will be thrown by itself
        }
    }

    void Interaction()
    {
        if (canPickup)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerAni.SetTrigger("Interaction");

                itemManager.AddItem(pickupItem);
                pickupObject.SetActive(false);

                ui.transform.GetChild(0).gameObject.SetActive(false);
                ui.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    public void PlayerIdle(bool isActive)
    {
        playerAni.SetBool("Idle", isActive);
    }
}
