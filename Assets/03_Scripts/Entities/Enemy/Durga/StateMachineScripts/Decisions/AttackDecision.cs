using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(menuName = "PluggableAI/Durga/Decision/ChaseOrAttack")]
public class AttackDecision : Decision
{
    public override bool Execute(StateMachineController controller)
    {
        return CheckForPlayer(controller);
    }

    public bool CheckForPlayer(StateMachineController controller)
    {
        if (Vector3.Distance(controller.settings.playerTarget.position, controller.transform.position) > controller.enemyStats.GetStatValue(StatName.Range))
        {
            // controller.animator.SetTrigger("cancel");

            controller.actions.Walk(controller);
            controller.agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
            return false;
        }

        controller.actions.StopWalking(controller);

        controller.agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

        return true;
    }
}
