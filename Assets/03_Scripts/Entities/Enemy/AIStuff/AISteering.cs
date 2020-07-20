using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISteering
{
    public Vector3 AvoidanceSteering(Vector3 dir, StateMachineController controller)
    {
        RaycastHit hit;

        if (!FindObstacle(dir, out hit, controller))
        {
            return controller.settings.playerTarget.position;
        }

        Vector3 targetpos = controller.settings.playerTarget.position;

        float angle = Vector3.Angle(dir * controller.deltaTime, hit.normal);
        if (angle > 165f)
        {
            Vector3 perp;

            perp = new Vector3(-hit.normal.z, hit.normal.y, hit.normal.x);

            targetpos = targetpos + (perp * Mathf.Sin((angle - 165f) * Mathf.Deg2Rad) * 2 * controller.settings.obstacleAvoidDistance);
        }

        return Seek(targetpos, controller);
    }

    public void IsGrounded(StateMachineController controller)
    {
        Vector3 velocity = Vector3.zero;
        if (!Physics.CheckSphere(controller.transform.position + new Vector3(0, 1, 0), 1.1f))
        {
            controller.isGrounded = false;
            DoGravity(controller, velocity);
        }
        else
        {
            controller.isGrounded = true;
        }
    }

    private void DoGravity(StateMachineController controller, Vector3 velocity)
    {
        velocity.y = Physics.gravity.y * Time.deltaTime;
        controller.transform.position += velocity;
    }

    Vector3 Seek(Vector3 targetPosition, StateMachineController controller)
    {
        Vector3 acceleration = SteerTowards(targetPosition, controller);

        acceleration.Normalize();

        return acceleration;
    }

    Vector3 SteerTowards(Vector3 vector, StateMachineController controller)
    {
        Vector3 v = vector.normalized * (controller.enemyStats.GetStatValue(StatName.Speed) * controller.enemyStats.GetMultValue(MultiplierName.speed));
        return Vector3.ClampMagnitude(v, controller.enemyStats.GetStatValue(StatName.Speed) * controller.enemyStats.GetMultValue(MultiplierName.speed));
    }

    bool FindObstacle(Vector3 dir, out RaycastHit hit, StateMachineController controller)
    {
        dir = dir.normalized;

        Vector3[] dirs = new Vector3[controller.settings.whiskerAmount];
        dirs[0] = dir;

        float orientation = VectorToOrientation(dir);
        float angle = orientation;
        for (int i = 1; i < (dirs.Length + 1) / 2; i++)
        {
            angle += controller.settings.angleIncrement;
            dirs[i] = OrientationToVector(orientation + angle * Mathf.Deg2Rad);
        }
        angle = orientation;
        for (int i = (dirs.Length + 1) / 2; i < dirs.Length; i++)
        {
            angle -= controller.settings.angleIncrement;
            dirs[i] = OrientationToVector(orientation - angle * Mathf.Deg2Rad);
        }
        return CastWhiskers(dirs, out hit, controller);
    }

    bool CastWhiskers(Vector3[] dirs, out RaycastHit firsthit, StateMachineController controller)
    {
        firsthit = new RaycastHit();
        for (int i = 0; i < dirs.Length; i++)
        {
            float dist = (i == 0) ? controller.settings.mainWhiskerL : controller.settings.secondaryWhiskerL;

            RaycastHit hit;

            if (Physics.SphereCast(controller.RayEmitter.position, 1f, dirs[i], out hit, dist, controller.settings.enemyMask))
            {
                firsthit = hit;
                return true;
            }
        }
        return false;
    }

    static Vector3 OrientationToVector(float orientation)
    {
        return new Vector3(Mathf.Cos(-orientation), 0, Mathf.Sin(-orientation));
    }

    static float VectorToOrientation(Vector3 direction)
    {
        return -1 * Mathf.Atan2(direction.z, direction.x);
    }
}
