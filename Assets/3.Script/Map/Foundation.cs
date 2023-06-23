using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundation : MonoBehaviour
{
    [HideInInspector] public bool isExist = false;
    [HideInInspector] public bool isBuildable = false; //placeable condition: isExist -> false, isBuildable -> true
    public bool isBuild = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FoundationInside"))
        {
            isBuildable = true;
        }
        if (other.CompareTag("Foundation"))
        {
            isExist = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("FoundationInside"))
        {
            isBuildable = true;
        }
        if (other.CompareTag("Foundation"))
        {
            isExist = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Foundation"))
        {
            isExist = false;
        }
        else if (other.CompareTag("FoundationInside"))
        {
            isBuildable = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Foundation"))
        {
            isExist = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Foundation"))
        {
            isExist = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Foundation"))
        {
            isExist = false;
        }
    }
}
