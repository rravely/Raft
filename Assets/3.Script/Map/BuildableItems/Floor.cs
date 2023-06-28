using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [HideInInspector] public bool isExist = false;
    [HideInInspector] public bool isBuildable = false; //placeable condition: isExist -> false, isBuildable -> true
    public bool isBuild = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            isExist = true;
        }
        else if (other.CompareTag("Pillar"))
        {
            isBuildable = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            isExist = true;
        }
        else if (other.CompareTag("Pillar"))
        {
            isBuildable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            isExist = false;
        }
        else if (other.CompareTag("Pillar"))
        {
            isBuildable = false;
        }
    }
}
