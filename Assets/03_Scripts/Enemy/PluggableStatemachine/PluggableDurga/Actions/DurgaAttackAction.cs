using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Durga/Action/Attack")]
public class DurgaAttackAction : Action
{
    public override void Execute(StateMachineController controller){
        controller.animator.SetInteger("Sequenz1", 1);
    }

    private void Attack(StateMachineController controller)
    {
        controller.animator.SetInteger("Sequenz1", 1);
    }


}
