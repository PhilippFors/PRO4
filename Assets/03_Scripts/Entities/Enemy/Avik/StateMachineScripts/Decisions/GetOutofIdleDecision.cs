using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/General/Decision/IdleDecision")]
public class GetOutofIdleDecision : Decision
{
    public override bool Execute(StateMachineController controller)
    {
        if (controller.settings.playerTarget != null)
        {
            controller.agent.isStopped = false;
            return true;
        }
        return false;
    }
}
