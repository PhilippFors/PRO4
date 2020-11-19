using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author("Philipp Forstner")]
[CreateAssetMenu(menuName = "PluggableAI/Avik/Action/IdleAction")]
public class AvikIdleAction : Action
{
    public override void Execute(StateMachineController controller){
        
        controller.agent.isStopped = false;
        controller.actions.Attack(controller, -1);
    }
}
