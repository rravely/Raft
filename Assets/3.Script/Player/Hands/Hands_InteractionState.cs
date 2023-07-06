using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands_InteractionState : StateMachineBehaviour
{
    SelectedItem selectedItem;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        selectedItem = FindObjectOfType<SelectedItem>();
        if (selectedItem.selectedItem.itemName.Equals("Hammer") || selectedItem.selectedItem.itemName.Equals("PlasticHook"))
        {
            selectedItem.SetActiveHammer(false);
            selectedItem.SetActiveHook(false);
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (selectedItem.selectedItem.itemName.Equals("Hammer") || selectedItem.selectedItem.itemName.Equals("PlasticHook"))
        {
            selectedItem.SetActiveHammer(false);
            selectedItem.SetActiveHook(false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (selectedItem.selectedItem.itemName.Equals("Hammer"))
        {
            selectedItem.SetActiveHammer(true);
        }
        else if (selectedItem.selectedItem.itemName.Equals("PlasticHook"))
        {
            selectedItem.SetActiveHook(true);
        }
    }
}
