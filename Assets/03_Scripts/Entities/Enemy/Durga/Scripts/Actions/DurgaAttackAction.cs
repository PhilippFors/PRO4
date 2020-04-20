using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Durga/Action/Attack")]
public class DurgaAttackAction : Action
{
    public override void Execute(StateMachineController controller){
        
        Attack(controller);
    }

    private void Attack(StateMachineController controller)
    {
        controller.weapon.isAttacking = true;
        controller.animator.SetInteger("Sequenz1", 1);
    }


}
