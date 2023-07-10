using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public bool isExist = false;
    public bool isBuildable = false; //placeable condition: isExist -> false, isBuildable -> true
    public bool isBuild = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            isExist = true;
        }
        else if (other.CompareTag("FoundationInside") || other.CompareTag("Floor"))
        {
            isBuildable = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            isExist = true;
        }
        else if (other.CompareTag("FoundationInside") || other.CompareTag("Floor"))
        {
            isBuildable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            isExist = false;
        }
        else if (other.CompareTag("FoundationInside") || other.CompareTag("Floor"))
        {
            isBuildable = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            isExist = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            isExist = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            isExist = false;
        }
    }
}
