using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/General/Decision/IdleDecision")]
public class GetOutofIdleDecision : Decision
{
    static float activationtimer = 0.5f;
    public override bool Execute(StateMachineController controller)
    {

        if (controller.aiManager.playerTarget != null & activationtimer <= 0)
        {
            controller.agent.isStopped = false;
            return true;
        }
        activationtimer -= controller.deltaTime;
        return false;
    }
}
