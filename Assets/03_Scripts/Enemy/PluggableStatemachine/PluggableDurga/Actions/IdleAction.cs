using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Durga/Action/IdleAction")]
public class IdleAction : Action
{
    public override void Execute(StateMachineController controller){
        controller.FindTarget();
    }
}
