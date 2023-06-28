using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    public bool isExist = false;
    public bool isBuildable = true; //placeable condition: isExist -> false, isBuildable -> true
    public bool isBuild = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Buildable"))
        {
            isExist = true;
            isBuildable = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Buildable"))
        {
            isExist = true;
            isBuildable = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pillar"))
        {
            isExist = false;
            isBuildable = true;
        }
        
    }
}
