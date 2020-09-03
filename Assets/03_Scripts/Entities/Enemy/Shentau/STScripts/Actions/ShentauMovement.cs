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

    Vector3 GetMovePos(StateMachineController controller)
    {
        //TODO: Procedural Grid on navemesh to next barrier
        //TODO: Grid holds info of stuff in close proximity like obstacles (or even player)

        return Vector3.zero;
    }

    void Move(StateMachineController controller, Vector3 movPos)
    {

    }


}
