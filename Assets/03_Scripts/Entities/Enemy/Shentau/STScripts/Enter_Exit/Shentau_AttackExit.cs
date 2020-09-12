using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shentau Attack Exit", menuName = "PluggableAI/Shentau/Enter_Exit/AttackExit")]

public class Shentau_AttackExit : OnExitState
{
    public override void Execute(StateMachineController controller)
    {
        controller.agent.isStopped = true;
        // controller.actions.GetAnimator().Play("tiltup");
    }

}
