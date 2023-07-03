using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark_Bite : StateMachineBehaviour
{
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Bite", false);
        animator.GetComponent<Shark>().isAttacking = false;
    }
}
