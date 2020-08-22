using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Avik/OnStatEnter_Exit/OnStunnedExit")]
public class Avik_OnStunnedExit : OnExitState
{
    public override void Execute(StateMachineController controller)
    {
        controller.agent.isStopped = false;
    }

}
