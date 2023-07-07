using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsRodAnimations_Rod_NoFishState : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("RodBack", false);
        animator.SetBool("RodDrop", false);
    }
}
