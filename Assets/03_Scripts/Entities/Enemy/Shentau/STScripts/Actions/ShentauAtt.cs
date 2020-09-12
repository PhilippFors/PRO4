using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Shentau/Action/Attack")]
public class ShentauAtt : Action
{
    public override void Execute(StateMachineController controller)
    {
        if (controller.actions.CheckIsAttacking(controller))
            controller.actions.Attack(controller);
    }

}
