using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author("Philipp Forstner")]
[CreateAssetMenu(menuName = "PluggableAI/Shentau/Action/Idle")]
public class ShentauIdle : Action
{
    public override void Execute(StateMachineController controller)
    {
        controller.agent.isStopped = false;
    }

}
