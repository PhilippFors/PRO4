using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author("Philipp Forstner")]
[CreateAssetMenu(fileName ="Shentau Attack Enter", menuName = "PluggableAI/Shentau/Enter_Exit/AttackEnter")]
public class Shentau_AttackEnter : OnEnterState
{
    public override void Execute(StateMachineController controller)
    {
        controller.agent.isStopped = true;
    }
}
