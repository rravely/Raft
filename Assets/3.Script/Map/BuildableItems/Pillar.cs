using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    [HideInInspector] public bool isExist = false; 
    [HideInInspector] public bool isBuildable = false; //placeable condition: isExist -> false, isBuildable -> true
    public bool isBuild = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pillar"))
        {
            isExist = true;
        }
        else if (other.CompareTag("Foundation"))
        {
            isBuildable = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pillar"))
        {
            isExist = true;
        }
        else if (other.CompareTag("Foundation"))
        {
            isBuildable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pillar"))
        {
            isExist = false;
        }
        else if (other.CompareTag("Foundation"))
        {
            isBuildable = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Pillar"))
        {
            isExist = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Pillar"))
        {
            isExist = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Pillar"))
        {
            isExist = false;
        }
    }
}
