using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticHook : MonoBehaviour
{
    public bool isThrowing = false;
    bool isThrown = false;

    Transform originalHook;

    [SerializeField] public Transform throwingHook;
    [SerializeField] PlayerInteraction playerInteraction;
    [SerializeField] Transform maxTarget;

    //Raycast
    int layerMaskWater;
    public Vector3 targetPos;
    Vector3 maxTargetPos;

    private void Start()
    {
        layerMaskWater = 1 << LayerMask.NameToLayer("Water");

        originalHook = transform.GetChild(0);

        maxTargetPos = maxTarget.position;
    }

    private void Update()
    {
        if (isThrowing && !isThrown && playerInteraction.isHookThrown)
        {
            isThrowing = false;

            //Set target position
            Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(960, 540)), out RaycastHit hit, 999f, layerMaskWater);
            targetPos = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            if ((transform.GetChild(1).position - targetPos).sqrMagnitude > 2.25f)
            {
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
