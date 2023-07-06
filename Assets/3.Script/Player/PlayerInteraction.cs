using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class PlayerInteraction : MonoBehaviour
{
    PlayerInput playerInput;
    InventoryInput inventoryInput;
    SelectedItem selectedItem;
    ItemManager itemManager;

    PlayerInputAction playerInputAction;
    InputAction interactionAction;

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

    [SerializeField] PlasticHook plasticHook;
    [SerializeField] Transform hookT;
    [SerializeField] Transform fakeHook;

    [Header("Hook")]
    bool isThrow = false;
    public bool isHookThrown = false;
    public bool isHookPull = false;
    public bool isHookBack = false;
    public bool isHooked = false;
    public bool canThrowHook = true;

    [Header("Pick up")]
    public bool canPickup = false;
    public Item pickupItem = null;
    public GameObject pickupObject = null;

    [Header("Purifier")]
    public bool isWater = false;
    [SerializeField] Item cupSaltWater;



    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        inventoryInput = GetComponent<InventoryInput>();
        selectedItem = GetComponent<SelectedItem>();
        itemManager = FindObjectOfType<ItemManager>();

        ui = GameObject.FindWithTag("InteractionUI");

        playerInputAction = new PlayerInputAction();
        interactionAction = playerInputAction.Player.Interaction;
    }

    private void Update()
    {
        Tools();
    }

    void Tools()
    {
        switch (selectedItem.selectedItem.itemName)
        {
            case "Hammer":
                playerAni.SetBool("Grab", true);
                Hammer();
                break;
            case "PlasticHook":
                if (!isThrow)
                {
                    playerAni.SetBool("Grab", true);
                }
                Hook();
                break;
            default:
                playerAni.SetBool("Grab", false);
                break;
        }
    }

    public void Hammer()
    {
        if (playerClickPanel.isPossibleClick)
        {
            if (playerInput.isLMDDown)
            {
                hammer.SetActive(true);

                playerAni.SetTrigger("Hammer");
                hammerAni.SetBool("Hammer", true);

                inventoryInput.DecreaseDurability(0.1f);
            }
        }
    }

    void Hook()
    {
        if (playerClickPanel.isPossibleClick)
        {
            if (playerInput.isLMDDown)
            {
                canThrowHook = true;
            }
            if (playerInput.isLMD && !isHookThrown && canThrowHook)
            {
                isThrow = true;

                playerAni.SetBool("Grab", false);
                playerAni.SetBool("HookCharging", true);
                hookAni.SetTrigger("Charge");
                if (charging < 100)
                {
                    charging += 1f;
                }
            }
            else if (isHookThrown && canThrowHook)
            {
                if (playerInput.isLMD)
                {
                    //Pull hook
                    isHookPull = true;
                    //isHookThrown = false;
                    playerAni.SetBool("HookCharging", false);
                }
                else
                {
                    isHookPull = false;
                }

                if (playerInput.isRMD && !isHooked)
                {
                    //Pull hook right away
                    isHookThrown = false;
                    canThrowHook = false;
                    plasticHook.ResetHook();
                    playerAni.SetBool("Grab", true);
                }


            }
        }

        if (playerInput.isLMDUp && charging > 5)
        {
            //Throw hook
            hookAni.SetTrigger("Throw");
            hookAni.SetBool("Charge", false);
            plasticHook.isThrowing = true;

            playerAni.SetTrigger("HookThrowing");
            playerAni.SetBool("HookCharging", false);

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

    void OnInteraction()
    {
        if (canPickup)
        {
            InteractionHands();

            itemManager.AddItem(pickupItem);
            pickupObject.SetActive(false);

            ui.transform.GetChild(0).gameObject.SetActive(false);
            ui.transform.GetChild(1).gameObject.SetActive(false);
        }

        if (selectedItem.selectedItem.itemName.Equals("CupEmpty") && isWater)
        {
            //Fill cup salt water
            itemManager.ChangeItem(selectedItem.selectedItem, cupSaltWater, selectedItem.selectedButtonIndex);
            selectedItem.selectedItem = cupSaltWater;

            //Play player animation
            InteractionHands();
        }
        else if (selectedItem.selectedItem.itemName.Equals("CupSaltWater"))
        {

        }
    }

    public void ResetHook()
    {
        plasticHook.ResetHook();
        isHookThrown = false;
        playerAni.SetBool("Grab", true);
    }

    public void PlayerIdle(bool isActive)
    {
        playerAni.SetBool("Idle", isActive);
    }

    public void GrabHands(bool isActive)
    {
        playerAni.SetBool("Grab", isActive);
    }

    public void HammerHands()
    {
        playerAni.SetTrigger("Hammer");
        hammerAni.SetBool("Hammer", true);
    }

    public void HammerHit(bool isActive)
    {
        hammerAni.SetBool("Hammer", isActive);
    }

    public void InteractionHands()
    {
        playerAni.SetTrigger("Interaction");
        hammer.SetActive(false);
        plasticHook.gameObject.SetActive(false);
    }

    public void AfterInteractionAnimation()
    {
        switch (selectedItem.selectedItem.itemName)
        {
            case "Hammer":
                hammer.SetActive(true);
                break;
            case "PlasticHook":
                plasticHook.gameObject.SetActive(true);
                break;
        }
    }
}
