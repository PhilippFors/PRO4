using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Durga/Action/Avoid")]
public class AvoidAction : Action
{
    public override void Execute(StateMachineController controller)
    {
        Vector3 offsetToTarget = (controller.target.position - controller.transform.position);

        Vector3 collisionAvoidDir = ObstacleRays(controller);
        Vector3 collisionAvoidForce = SteerTowards(collisionAvoidDir, controller) * 0.5f;
        Vector3 target = SteerTowards(offsetToTarget, controller) * 0.5f;


        Vector3 acceleration = Vector3.zero;
        acceleration = collisionAvoidForce;
        acceleration += target;

        Vector3 velocity = acceleration * controller.deltaTime;
        float speed = velocity.magnitude;
        Vector3 dir = velocity / speed;
        speed = Mathf.Clamp(speed, controller.enemyStats.GetStatValue(StatName.Speed), controller.enemyStats.GetStatValue(StatName.Speed));
        velocity = dir * speed;

        Vector3 meanDir = new Vector3(((offsetToTarget.x + collisionAvoidDir.x) / 2), 0, ((offsetToTarget.z + collisionAvoidDir.z) / 2));

        controller.agent.destination = controller.offsetTargetPos;

        Vector3 moveTo = velocity * controller.deltaTime;

        controller.agent.Move(moveTo);
    }

    Vector3[] BoidHelper(StateMachineController controller)
    {
        Vector3[] directions = new Vector3[controller.settings.rayAmount];

        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;

        for (int i = 0; i < directions.Length; i++)
        {
            float t = (float)i / controller.settings.rayAmount;
            float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = angleIncrement * i;

            float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float z = Mathf.Cos(inclination);
            directions[i] = new Vector3(x, 0, z);
        }

        return directions;
    }

    Vector3 ObstacleRays(StateMachineController controller)
    {
        Vector3[] rayDirections = BoidHelper(controller);
        for (int i = 0; i < rayDirections.Length; i++)
        {
            // if (controller.avoidDirection)
            // {
            //     controller.RayEmitter.localRotation = Quaternion.Slerp(controller.RayEmitter.localRotation, Quaternion.Euler(new Vector3(0, -30f, 0)), 1f);
            // }
            // else
            // {
            //     controller.RayEmitter.localRotation = Quaternion.Slerp(controller.RayEmitter.localRotation, Quaternion.Euler(new Vector3(0, 30f, 0)), 1f);
            // }
            // RaycastHit hit;
            // if (!Physics.SphereCast(controller.RayEmitter.position, 1f, controller.transform.forward, out hit, 4f, controller.enemyMask))
            // {
            //     controller.RayEmitter.localRotation = Quaternion.Slerp(controller.RayEmitter.localRotation, Quaternion.Euler(new Vector3(0, 0, 0)), 1f);
            // }
            Vector3 dir = controller.RayEmitter.TransformDirection(rayDirections[i]);


            Ray ray = new Ray(controller.RayEmitter.position, dir);

            if (!Physics.Raycast(ray, 4f, controller.enemyMask))
            {
                Debug.DrawRay(controller.RayEmitter.position, dir, Color.green);

                return dir;
            }
            else
            {
                Debug.DrawRay(controller.RayEmitter.position, dir, Color.red);
            }
        }

        return controller.transform.forward;
    }

    Vector3 SteerTowards(Vector3 vector, StateMachineController controller)
    {
        Vector3 v = vector.normalized * (controller.enemyStats.GetStatValue(StatName.Speed) * controller.enemyStats.GetMultValue(MultiplierName.speed));
        return Vector3.ClampMagnitude(v, controller.enemyStats.GetStatValue(StatName.Speed) * controller.enemyStats.GetMultValue(MultiplierName.speed));
    }
}


