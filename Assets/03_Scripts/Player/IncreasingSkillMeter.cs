using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasingSkillMeter : StateMachineBehaviour
{
    private static PlayerAttack attack;

// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public new void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        if (stateInfo.IsTag("Combo1") && attack.comboCounter >= 4)
        {
            attack.skills[0].current += 2;
        }

        else if (stateInfo.IsTag("Combo2") && attack.comboCounter >= 4)
        {
            attack.skills[1].current += 2;
        }

        else if (stateInfo.IsTag("Combo3") && attack.comboCounter >= 4)
        {
            attack.skills[2].current += 2;
        }
    }


// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//{
//    
//}

// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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