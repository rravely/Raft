using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedSimpleCollider : MonoBehaviour
{
    bool canSleep = false;

    private void Update()
    {
        if (canSleep)
        {
            //sleep ui
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canSleep = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canSleep = false;
        }
    }
}
