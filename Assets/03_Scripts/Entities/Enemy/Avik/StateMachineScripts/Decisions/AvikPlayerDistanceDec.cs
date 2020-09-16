using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(menuName = "PluggableAI/Avik/Decision/PlayerDistance")]
public class AvikPlayerDistanceDec : Decision
{
    public override bool Execute(StateMachineController controller)
    {
        return CheckForPlayer(controller);
    }

    public bool CheckForPlayer(StateMachineController controller)
    {
        if (!CheckInFront(controller) && Vector3.Distance(controller.aiManager.playerTarget.position, controller.transform.position) > controller.enemyStats.GetStatValue(StatName.Range))
        {
            if (!controller.actions.CheckIsAttacking(controller))
            {
                controller.actions.Walk(controller);
                controller.agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;

                return false;
            }
            else
            {
                controller.actions.StopWalking(controller);

                controller.agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
                return true;
            }
        }
        else
        {

            controller.actions.StopWalking(controller);

            controller.agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
            return true;

        }
    }

    bool CheckInFront(StateMachineController controller)
    {
        Ray ray = new Ray(controller.RayEmitter.position, controller.transform.forward);
        return Physics.SphereCast(ray, 0.2f, controller.enemyStats.GetStatValue(StatName.Range), LayerMask.GetMask("Player"));
    }
}
