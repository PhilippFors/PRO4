using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetValues : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("Sequenz1", 0);
        animator.ResetTrigger("cancel");
        
    }

}
