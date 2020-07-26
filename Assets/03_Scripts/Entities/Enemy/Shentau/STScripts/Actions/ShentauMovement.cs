using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Shentau/Action/Move")]
public class ShentauMovement : Action
{
    public override void Execute(StateMachineController controller)
    {
        Move(controller, GetMovePos(controller));
    }

    Vector3 GetMovePos(StateMachineController controller){


        return Vector3.zero;
    }

    void Move(StateMachineController controller, Vector3 movPos){

    }


}
