using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISteering
{
    public Vector3 GetSteering(Vector3 dir, StateMachineController controller)
    {
        RaycastHit hit;

        if (!FindObstacle(dir, out hit, controller))
        {
            return controller.target.position;
        }

        Vector3 targetpos = controller.target.position;

        float angle = Vector3.Angle(dir * controller.deltaTime, hit.normal);
        if (angle > 165f)
        {
            Vector3 perp;

            perp = new Vector3(-hit.normal.z, hit.normal.y, hit.normal.x);

            targetpos = targetpos + (perp * Mathf.Sin((angle - 165f) * Mathf.Deg2Rad) * 2 * controller.settings.obstacleAvoidDistance);
        }

        return Seek(targetpos, controller);
    }

    public Vector3 Seek(Vector3 targetPosition, StateMachineController controller)
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

        Vector3[] dirs = new Vector3[11];
        dirs[0] = dir;

        float orientation = VectorToOrientation(dir);
        float angle = orientation;
        for (int i = 1; i < 6; i++)
        {
            angle += 10f;
            dirs[i] = OrientationToVector(orientation + angle * Mathf.Deg2Rad);
        }
        angle = orientation;
        for (int i = 6; i <dirs.Length; i++)
        {
            angle -= 10f;
            dirs[i] = OrientationToVector(orientation - angle * Mathf.Deg2Rad);
        }
        return CastWhiskers(dirs, out hit, controller);
    }

    bool CastWhiskers(Vector3[] dirs, out RaycastHit firsthit, StateMachineController controller)
    {
        firsthit = new RaycastHit();
        bool foundObs = false;

        for (int i = 0; i < dirs.Length; i++)
        {
            float dist = (i == 0) ? 4f : 2f;

            RaycastHit hit;

            if (Physics.Raycast(controller.RayEmitter.position, dirs[i],out hit, 4f, controller.enemyMask))
            {
                foundObs = true;
                firsthit = hit;
                break;
            }
        }
        return foundObs;
    }

    public static Vector3 OrientationToVector(float orientation)
    {
        return new Vector3(Mathf.Cos(-orientation), 0, Mathf.Sin(-orientation));
    }

    public static float VectorToOrientation(Vector3 direction)
    {
        return -1 * Mathf.Atan2(direction.z, direction.x);
    }
}
