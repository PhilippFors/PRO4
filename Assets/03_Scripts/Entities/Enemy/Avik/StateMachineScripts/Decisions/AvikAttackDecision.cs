using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(menuName = "PluggableAI/Avik/Decision/ChaseOrAttack")]
public class AvikAttackDecision : Decision
{
    public override bool Execute(StateMachineController controller)
    {
        controller.actions.CheckIsAttacking(controller);
        return CheckForPlayer(controller);
    }

    public bool CheckForPlayer(StateMachineController controller)
    {
        if (Vector3.Distance(controller.settings.playerTarget.position, controller.transform.position) > controller.enemyStats.GetStatValue(StatName.Range) &!controller.isAttacking)
        {
            controller.actions.Walk(controller);
            controller.agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
            return false;
        }

        controller.actions.StopWalking(controller);

        controller.agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

        return true;
    }
}
