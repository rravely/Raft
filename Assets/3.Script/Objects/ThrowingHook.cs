using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingHook : MonoBehaviour
{
    PlayerInteraction playerInteraction;
    ItemManager itemManager;

    [Header("Player hook end point")]
    [SerializeField] Transform player;

    public List<GameObject> dropItem;

    bool isLock = false;

    private void Start()
    {
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        itemManager = FindObjectOfType<ItemManager>();
    }

    private void Update()
    {
        if (playerInteraction.isHookPull)
        {
            if ((player.position - transform.position).sqrMagnitude > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, 0.005f);
            }
            else //When hook enters player range
            {
                if (dropItem.Count > 0)
                {
                    //AddItem
                    for (int i = 0; i < dropItem.Count; i++)
                    {
                        itemManager.AddItem(dropItem[i].GetComponent<DropItem>().dropItem);
                        dropItem[i].GetComponent<DropItem>().isHooked = false;
                        //Set inactive dropItem object
                        dropItem[i].SetActive(false);
                    }
                    dropItem.Clear();
                }
                //Reset Hook position
                playerInteraction.ResetHook();
                playerInteraction.isHookPull = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DropItem"))
        {
            if (!other.GetComponent<DropItem>().isHooked)
            {
                dropItem.Add(other.gameObject);
                other.GetComponent<DropItem>().isHooked = true;
            }
        }
    }
}
