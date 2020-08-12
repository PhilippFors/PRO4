using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/General/OnStatEnter_Exit/AttackExit")]
public class OnAttackExit : OnExitState
{
    public override void Execute(StateMachineController controller)
    {
        controller.actions.CancelAttack(controller);
    }
}
