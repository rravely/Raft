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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Buildable"))
        {
            isBuildable = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Buildable"))
        {
            isBuildable = true;
        }
    }
}
