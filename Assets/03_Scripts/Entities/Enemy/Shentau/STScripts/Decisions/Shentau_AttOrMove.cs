using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Shentau/Decision/AttOrMove")]
public class Shentau_AttOrMove : Decision
{
    public override bool Execute(StateMachineController controller)
    {
        if (controller.actions.GetAttackCountdown() > 0)
            return true;
        return false;
    }

}
