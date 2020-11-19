using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author("Philipp Forstner")]
[CreateAssetMenu(menuName = "PluggableAI/Shentau/Action/ChargeAttack")]
public class ShentauChargeAtt : Action
{
    public override void Execute(StateMachineController controller)
    {
        Move(controller);
        controller.actions.Attack(controller, 1);
    }
    Vector3 GetMovePos(StateMachineController controller)
    {
        if (CollisionCourse(controller))
        {
            return controller.steering.AvoidanceSteering(controller.transform.forward, controller);
        }
        else
        {
            Vector3 dir = controller.transform.position - controller.aiManager.playerTarget.position;
            return controller.transform.position + dir;
        }
    }

    bool CollisionCourse(StateMachineController controller)
    {
        RaycastHit hit;
        return Physics.SphereCast(controller.transform.position, 0.5f, controller.transform.forward, out hit, 1.5f);
    }

    void Move(StateMachineController controller)
    {

        if (Vector3.Distance(controller.aiManager.playerTarget.position, controller.transform.position) > 5f & Vector3.Distance(controller.aiManager.playerTarget.position, controller.transform.position) < 8f)
        {
            controller.agent.isStopped = true;
            Vector3 dir = controller.aiManager.playerTarget.position - controller.transform.position;
            Quaternion look = Quaternion.LookRotation(dir);
            controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, look, controller.deltaTime * controller.enemyStats.GetStatValue(StatName.TurnSpeed));

        }
        else if (Vector3.Distance(controller.aiManager.playerTarget.position, controller.transform.position) > 8f)
        {
            controller.agent.isStopped = false;
            Vector3 dir = controller.aiManager.playerTarget.position - controller.transform.position;
            Quaternion look = Quaternion.LookRotation(dir);
            controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, look, controller.deltaTime * controller.enemyStats.GetStatValue(StatName.TurnSpeed));
            controller.agent.destination = controller.aiManager.playerTarget.position;
            Vector3 moveTo = controller.transform.forward * (controller.enemyStats.GetStatValue(StatName.Speed) * controller.enemyStats.GetMultValue(MultiplierName.speed)) * controller.deltaTime;
            controller.agent.Move(moveTo);
        }
        else if (Vector3.Distance(controller.aiManager.playerTarget.position, controller.transform.position) < 5f)
        {
            controller.agent.isStopped = false;
            Vector3 dir = controller.aiManager.playerTarget.position - controller.transform.position;
            Quaternion look = Quaternion.LookRotation(dir);
            controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, look, controller.deltaTime * controller.enemyStats.GetStatValue(StatName.TurnSpeed));
            controller.agent.destination = GetMovePos(controller);
            Vector3 moveTo = -dir.normalized * (controller.enemyStats.GetStatValue(StatName.Speed) * controller.enemyStats.GetMultValue(MultiplierName.speed)) * controller.deltaTime;
            controller.agent.Move(moveTo);
        }
    }

}
