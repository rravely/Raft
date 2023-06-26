using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingHookState : StateMachineBehaviour
{
    PlasticHook plasticHook;
    Transform parent;
    Transform ropePos;
    GameObject rope;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        plasticHook = FindObjectOfType<PlasticHook>();

        parent = animator.transform.parent;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ((parent.transform.position - plasticHook.targetPos).sqrMagnitude > 0.01f)
        {
            parent.position = Vector3.MoveTowards(parent.position, plasticHook.targetPos, 0.02f);
        }
    }
}
