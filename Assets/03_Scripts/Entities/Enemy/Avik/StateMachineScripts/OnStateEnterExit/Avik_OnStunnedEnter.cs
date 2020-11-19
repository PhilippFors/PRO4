using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Author("Philipp Forstner")]

[CreateAssetMenu(menuName = "PluggableAI/Avik/OnStatEnter_Exit/OnStunnedEnter")]
public class Avik_OnStunnedEnter : OnEnterState
{
    public override void Execute(StateMachineController controller)
    {
        controller.checkAnyTransition = false;
        controller.actions.Stunned(controller);
        Debug.Log("Stunned");
        controller.actions.CancelAttack(controller);
        controller.agent.isStopped = true;
    }
}
