using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Avik/Action/IdleAction")]
public class AvikIdleAction : Action
{
    public override void Execute(StateMachineController controller){
        
        controller.agent.isStopped = false;
    }
}
