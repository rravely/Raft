using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKControl : MonoBehaviour
{
    Animator playerAni;

    public Transform toolPivot;
    public Transform rightHand;

    private void Start()
    {
        playerAni = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        toolPivot.position = playerAni.GetIKHintPosition(AvatarIKHint.RightElbow);

        playerAni.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAni.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        playerAni.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
        playerAni.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);
    }
}
