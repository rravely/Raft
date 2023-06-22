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

        //이왕이면 rope도 같이 해줬으면 좋겠다 ㅎ
        while (playerInteraction.charging > 0)
        {
            //target position으로 이동하렴 ^^
            //MoveTowards

            playerInteraction.charging -= 0.1f;
            yield return null;
        }

        playerInteraction.charging = 0f;
        isThrowing = false;
        isThrown = true;
    }
}
