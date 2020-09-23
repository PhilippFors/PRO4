using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/General/Decision/AvoidDecision")]
public class AvoidDecision : Decision
{
    public override bool Execute(StateMachineController controller)
    {
        if (IsHeadingForCollision(controller))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool IsHeadingForCollision(StateMachineController controller)
    {
        Vector3 dir = Vector3.zero;
        if (Vector3.Distance(controller.aiManager.playerTarget.position, controller.transform.position) < controller.aiManager.avoidDistance)
        {
            dir = controller.aiManager.playerTarget.position - controller.transform.position;
            dir = dir.normalized;
            RaycastHit hit;
            if (Physics.SphereCast(controller.transform.position, 0.5f, dir, out hit, 3f, controller.aiManager.enemyMask))
            {
                Debug.DrawRay(controller.transform.position, dir, Color.yellow);
                
                return true;
            }
        }
        return false;
    }
}
