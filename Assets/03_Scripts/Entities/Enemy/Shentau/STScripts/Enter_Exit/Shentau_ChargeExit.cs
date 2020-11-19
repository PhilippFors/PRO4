using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Author("Philipp Forstner")]
[CreateAssetMenu(fileName = "Shentau Charge Exit", menuName = "PluggableAI/Shentau/Enter_Exit/ChargeExit")]
public class Shentau_ChargeExit : OnExitState
{
    public override void Execute(StateMachineController controller)
    {
        controller.enemyStats.SetStatValue(StatName.Speed, 3f);
    }
}
