using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Durga/Decision/ChaseOrAttack")]
public class AttackDecision : Decision
{
    public override bool ExecuteDecision(StateMachineController controller)
    {
        return CheckForPlayer(controller);
    }

    public bool CheckForPlayer(StateMachineController controller)
    {
        if (Vector3.Distance(controller.target.position, controller.transform.position) > controller.enemystats.GetStatValue(StatName.range))
        {
            controller.animator.SetTrigger("cancel");
            return false;

        }
        return true;
    }
}
