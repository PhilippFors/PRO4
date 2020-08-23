using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Avik/OnStatEnter_Exit/OnStunnedEnter")]
public class Avik_OnStunnedEnter : OnEnterState
{
    public override void Execute(StateMachineController controller)
    {
        controller.actions.CancelAttack(controller);
        controller.agent.isStopped = true;
    }
}
