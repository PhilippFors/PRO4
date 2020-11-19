using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author("Philipp Forstner")]
[CreateAssetMenu(fileName ="Shentau Charge Enter", menuName = "PluggableAI/Shentau/Enter_Exit/ChargeEnter")]
public class Shentau_ChargeEnter : OnEnterState
{
    public override void Execute(StateMachineController controller)
    {
        controller.enemyStats.SetStatValue(StatName.Speed, 1f);
        controller.actions.Attack(controller, 2);
        // controller.actions.GetAnimator().Play("tiltdown");
    }
}
