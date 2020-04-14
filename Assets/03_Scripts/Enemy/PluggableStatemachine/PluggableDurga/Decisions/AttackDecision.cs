using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Durga/Decision/ChaseOrAttack")]
public class AttackDecision : Decision
{
    public override bool ExecuteDecision(StateMachineController controller){
        if (Vector3.Distance(controller.target.position, controller.transform.position) < controller.enemystats.GetStatValue(StatName.range))
        {
            return true;
        }
        controller.animator.SetTrigger("cancel");
        return false;
    }
}
