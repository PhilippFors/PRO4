using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author("Philipp Forstner")]
[CreateAssetMenu(menuName = "PluggableAI/General/OnStatEnter_Exit/AvoidEnter")]
public class OnAvoidStateEnter : OnEnterState
{
    public override void Execute(StateMachineController controller)
    {
        Vector3 dir = controller.aiManager.playerTarget.position - controller.transform.position;
        Vector3 newDir = dir;
        int rightAmount = 0;
        int leftAmount = 0;
        for (int i = 0; i < 5; i++)
        {
            newDir.y += 5f;
            RaycastHit[] amount = Physics.SphereCastAll(controller.transform.position, 1f, dir, 5f, controller.aiManager.enemyMask);
            rightAmount += amount.Length;
        }
        newDir = dir;
        for (int i = 0; i < 5; i++)
        {
            newDir.y -= 5f;
            RaycastHit[] amount = Physics.SphereCastAll(controller.transform.position, 1f, dir, 5f, controller.aiManager.enemyMask);
            leftAmount += amount.Length;
        }

        if (rightAmount > leftAmount)
        {
            controller.avoidDirection = true;
        }
        else
        {
            controller.avoidDirection = false;
        }
        controller.checkedAmount = true;
    }
}
