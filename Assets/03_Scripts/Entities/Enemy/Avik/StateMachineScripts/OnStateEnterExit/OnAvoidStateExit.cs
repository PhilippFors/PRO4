using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author("Philipp Forstner")]
[CreateAssetMenu(menuName = "PluggableAI/General/OnStatEnter_Exit/AvoidExit")]
public class OnAvoidStateExit : OnExitState
{
    public override void Execute(StateMachineController controller)
    {
       controller.checkedAmount = false;
    }
}
