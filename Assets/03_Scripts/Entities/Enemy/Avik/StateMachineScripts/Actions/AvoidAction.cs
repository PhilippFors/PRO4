using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/General/Action/Avoid")]
public class AvoidAction : Action
{
    public override void Execute(StateMachineController controller)
    {
        // Vector3 offsetToTarget = (controller.target.position - controller.transform.position);

        // Vector3 collisionAvoidDir = ObstacleRays(controller);
        // Vector3 collisionAvoidForce = SteerTowards(collisionAvoidDir, controller) * 0.5f;
        // Vector3 target = SteerTowards(offsetToTarget, controller) * 0.5f;


        // Vector3 acceleration = Vector3.zero;
        // acceleration = collisionAvoidForce;
        // acceleration += target;

        // Vector3 velocity = acceleration * controller.deltaTime;
        // float speed = velocity.magnitude;
        // Vector3 dir = velocity / speed;
        // speed = Mathf.Clamp(speed, controller.enemyStats.GetStatValue(StatName.Speed), controller.enemyStats.GetStatValue(StatName.Speed));
        // velocity = dir * speed;

        // Vector3 meanDir = new Vector3(((offsetToTarget.x + collisionAvoidDir.x) / 2), 0, ((offsetToTarget.z + collisionAvoidDir.z) / 2));

        controller.agent.destination = controller.steering.AvoidanceSteering(controller.transform.forward, controller);
        

        Vector3 moveTo = controller.transform.forward * (controller.enemyStats.GetStatValue(StatName.Speed) * controller.enemyStats.GetMultValue(MultiplierName.speed)) * controller.deltaTime;

        controller.agent.Move(moveTo);
    }

}


