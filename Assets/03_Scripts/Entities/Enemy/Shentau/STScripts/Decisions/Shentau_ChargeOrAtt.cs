using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Shentau/Decision/ChargeOrAtt")]
public class Shentau_ChargeOrAtt : Decision
{
    public override bool Execute(StateMachineController controller)
    {
        return controller.actions.CheckIsAttacking(controller);
    }

}
