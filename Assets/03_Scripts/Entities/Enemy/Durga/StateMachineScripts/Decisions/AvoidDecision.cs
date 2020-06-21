using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Durga/Decision/AvoidDecision")]
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
        if (Vector3.Distance(controller.target.position, controller.transform.position) < controller.settings.avoidDistance)
        {
            dir = controller.target.position - controller.transform.position;
            dir = dir.normalized;
            RaycastHit hit;
            if (Physics.SphereCast(controller.transform.position, 0.5f, dir, out hit, 3f, controller.enemyMask))
            {
                Debug.DrawRay(controller.transform.position, dir, Color.yellow);
                
                return true;
            }
        }
        return false;
    }
}
