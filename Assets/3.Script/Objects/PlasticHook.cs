using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticHook : MonoBehaviour
{
    public bool isThrowing = false;
    bool isThrown = false;

    Transform originalHook, throwingHook;

    [SerializeField] PlayerInteraction playerInteraction;
    [SerializeField] Transform maxTarget;

    //Raycast
    int layerMaskWater;
    Vector3 targetPos, maxTargetPos;

    private void Start()
    {
        layerMaskWater = 1 << LayerMask.NameToLayer("Water");

        originalHook = transform.GetChild(0);
        throwingHook = transform.GetChild(1);

        maxTargetPos = maxTarget.position;
    }

    private void Update()
    {
        if (isThrowing && !isThrown && playerInteraction.isHookThrown)
        {
            StartCoroutine(Throwing_co());
        }    
    }

    IEnumerator Throwing_co()
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
        originalHook.transform.SetParent(null);
        //originalHook.gameObject.SetActive(false);
        //throwingHook.gameObject.SetActive(true);

        //�̿��̸� rope�� ���� �������� ���ڴ� ��
        while ((originalHook.transform.position - targetPos).sqrMagnitude > 0.01f)
        {
            //target position���� �̵��Ϸ� ^^
            //MoveTowards
            originalHook.transform.position = Vector3.MoveTowards(originalHook.transform.position, targetPos, 0.05f);
            
            yield return null;
        }

        playerInteraction.charging = 0f;
        isThrown = true;
    }
}
