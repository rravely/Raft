using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands_GrabState : StateMachineBehaviour
{
    SelectedItem selectedItem;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        selectedItem = FindObjectOfType<SelectedItem>();
        switch (selectedItem.selectedItem.itemName)
        {
            case "Hammer":
                selectedItem.SetActiveHammer(true);
                selectedItem.SetActiveHook(false);
                break;
            case "PlasticHook":
                selectedItem.SetActiveHammer(false);
                selectedItem.SetActiveHook(true);
                break;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch (selectedItem.selectedItem.itemName)
        {
            case "Hammer":
                selectedItem.SetActiveHammer(true);
                selectedItem.SetActiveHook(false);
                break;
            case "PlasticHook":
                selectedItem.SetActiveHammer(false);
                selectedItem.SetActiveHook(true);
                break;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        selectedItem.SetActiveHammer(false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
