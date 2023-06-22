using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableItem : MonoBehaviour
{
    [HideInInspector] public bool isBuildable = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Buildable"))
        {
            isBuildable = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Buildable"))
        {
            isBuildable = true;
        }
    }
}
