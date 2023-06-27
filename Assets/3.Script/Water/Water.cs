using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] Material water;
    Material sky;

    private void Start()
    {
        sky = RenderSettings.skybox;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerState>().inWater)
            {
                other.GetComponent<PlayerState>().inWater = false;
                other.GetComponent<PlayerState>().inWaterSurface = true;

                RenderSettings.skybox = sky;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponent<PlayerState>().inWater)
            {
                other.GetComponent<PlayerState>().inWater = true;
                //Turn off player's rigidbody after 0.2f seconds
                StartCoroutine(TurnOffPlayerRigidbody(other.GetComponent<Rigidbody>()));

                RenderSettings.skybox = water;
            }
        }
    }
    IEnumerator TurnOffPlayerRigidbody(Rigidbody otherRigid)
    {
        yield return new WaitForSeconds(0.2f);
        otherRigid.useGravity = false;
        otherRigid.velocity = Vector3.down * 0.05f;
    }

}
