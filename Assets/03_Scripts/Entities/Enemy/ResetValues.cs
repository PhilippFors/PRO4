using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetValues : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("attacknr", 0);
        animator.ResetTrigger("cancel");
        animator.ResetTrigger("comboTrigger");
        animator.ResetTrigger("stop");
    }
}
