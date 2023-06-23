using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemInactive : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DropItem"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
