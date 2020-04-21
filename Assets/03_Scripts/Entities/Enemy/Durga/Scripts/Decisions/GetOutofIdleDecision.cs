using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Durga/Decision/IdleDecision")]
public class GetOutofIdleDecision : Decision
{
    public override bool Execute(StateMachineController controller)
    {
        if (controller.target != null)
        {
            controller.agent.isStopped = false;
            return true;
        }
        return false;
    }
}
