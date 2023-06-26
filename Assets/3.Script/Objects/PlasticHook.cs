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

    //Calculate vector
    Vector3 playerDir;

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
            #region Using Raycast
            /*
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
            */
            #endregion

            playerDir = new Vector3(Mathf.Sin(playerInteraction.transform.eulerAngles.y * Mathf.Deg2Rad), 0f, Mathf.Cos(playerInteraction.transform.eulerAngles.y * Mathf.Deg2Rad));
            Vector3 target;
            if (playerInteraction.charging < 30)
            {
                target = playerDir * playerInteraction.charging * 0.1f;
            }
            else
            {
                target = playerDir * playerInteraction.charging * 0.03f;
            }
            targetPos = new Vector3(target.x + playerInteraction.transform.position.x - 0.1f, -0.48f, target.z + playerInteraction.transform.position.z);

            //Set parent
            throwingHook.transform.SetParent(null);
            throwingHook.gameObject.SetActive(true);
            originalHook.gameObject.SetActive(false);

            playerInteraction.charging = 0f;
            isThrown = true;
        }
    }

    public void ResetHook()
    {
        //fakehook come back
        throwingHook.SetParent(transform.parent);
        throwingHook.localPosition = new Vector3(0f, 0f, 0f);
        throwingHook.localEulerAngles = new Vector3(0f, 0f, 0f);
        throwingHook.gameObject.SetActive(false);
        originalHook.gameObject.SetActive(true);

        playerInteraction.isHookBack = false;
        isThrown = false;
    }
}
