using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author("Philipp Forstner")]
[CreateAssetMenu(menuName = "PluggableAI/Shentau/Decision/ChargeOrMove")]
public class Shentau_ChargeOrMove : Decision
{
    public override bool Execute(StateMachineController controller)
    {
        if (controller.actions.GetAttackCountdown() < 2f)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
