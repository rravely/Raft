using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticHook : MonoBehaviour
{
    public bool isThrowing = false;
    bool isThrown = false;

    Transform originalHook;

    [Header("Fake Hook")]
    [SerializeField] public Transform throwingHook;
    [Header("Target max position")]
    [SerializeField] Transform maxTarget;
    [Header("Player")]
    [SerializeField] PlayerInteraction playerInteraction;

    //Raycast
    int layerMaskWater;
    public Vector3 targetPos;
    Vector3 maxTargetPos;
    float maxDist;

    private void Start()
    {
        layerMaskWater = 1 << LayerMask.NameToLayer("Water");

        originalHook = transform.GetChild(0);

        maxDist = (transform.GetChild(1).position - maxTarget.position).sqrMagnitude;
    }

    private void Update()
    {
        if (isThrowing && !isThrown && playerInteraction.isHookThrown)
        {
            isThrowing = false;

            //Set target position
            Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(960, 540)), out RaycastHit hit, 999f, layerMaskWater);
            targetPos = hit.point;
            if ((transform.GetChild(1).position - targetPos).sqrMagnitude < maxDist)
            {
                targetPos = new Vector3(hit.point.x - 0.13f, hit.point.y - 0.02f, hit.point.z);
            }
            else
            {
                maxTargetPos = new Vector3(maxTarget.position.x - 0.13f, maxTarget.position.y - 0.02f, maxTarget.position.z);
                targetPos = maxTargetPos;
            }

            //Set parent
            throwingHook.transform.SetParent(null);
            throwingHook.gameObject.SetActive(true);
            originalHook.gameObject.SetActive(false);

            playerInteraction.charging = 0f;
            isThrown = true;
        }    
    }
}
