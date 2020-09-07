using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(menuName = "PluggableAI/Shentau/Action/Move")]
public class ShentauMovement : Action
{
    public override void Execute(StateMachineController controller)
    {
        Move(controller);
    }

    Vector3 GetMovePos(StateMachineController controller)
    {
        float minWaypointDistX = Random.Range(-5, 6);
        float minWaypointDistZ = Random.Range(-5, 6);
        Vector3 pos = controller.transform.position;

        NavMeshHit hit;
        Vector3 newWapoint = new Vector3(pos.x + minWaypointDistX, pos.y, pos.z + minWaypointDistZ);
        if (controller.agent.Raycast(newWapoint, out hit))
        {
            minWaypointDistX = Random.Range(-5, 6);
            minWaypointDistZ = Random.Range(-5, 6);
            newWapoint = new Vector3(pos.x + minWaypointDistX, pos.y, pos.z + minWaypointDistZ);

            Ray ray = new Ray(controller.transform.position, newWapoint - controller.transform.position);
            RaycastHit rayhit;
            if (Physics.Raycast(controller.transform.position, newWapoint - controller.transform.position, out rayhit, 7f))
            {
                if (rayhit.transform.GetComponent<AreaBarrier>() != null)
                {
                    newWapoint = new Vector3(newWapoint.x < 0 ? newWapoint.x - 2f : newWapoint.x + 2f, newWapoint.y, newWapoint.z < 0 ? newWapoint.z - 2f : newWapoint.z + 2f);
                }
            }
            return newWapoint;
        }
        return pos;
    }

    void Move(StateMachineController controller)
    {
        // if (controller.agent.destination == null)
        //     controller.agent.destination = GetMovePos(controller);


        if (Vector3.Distance(controller.settings.playerTarget.position, controller.transform.position) < 6f)
        {
            if (controller.agent.remainingDistance <= 0.1)
                controller.agent.destination = GetMovePos(controller);

            Vector3 moveTo = controller.transform.forward * (controller.enemyStats.GetStatValue(StatName.Speed) * controller.enemyStats.GetMultValue(MultiplierName.speed)) * controller.deltaTime;
            controller.agent.Move(moveTo);
        }
        else
        {
            Vector3 dir = controller.settings.playerTarget.position - controller.transform.position;
            Quaternion look = Quaternion.LookRotation(dir);
            controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, look, controller.deltaTime * controller.enemyStats.GetStatValue(StatName.TurnSpeed));
        }

    }


}
