using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticHook : MonoBehaviour
{
    bool isThrowing = false;
    bool isThrown = false;

    [SerializeField] PlayerInteraction playerInteraction;

    private void Update()
    {
        if (!isThrowing && playerInteraction.isHookThrown)
        {
            StartCoroutine(Throwing_co());
        }    
    }

    IEnumerator Throwing_co()
    {
        //target position

        //�̿��̸� rope�� ���� �������� ���ڴ� ��
        while (playerInteraction.charging > 0)
        {
            //target position���� �̵��Ϸ� ^^
            //MoveTowards

            playerInteraction.charging -= 0.1f;
            yield return null;
        }

        playerInteraction.charging = 0f;
        isThrowing = false;
        isThrown = true;
    }
}
