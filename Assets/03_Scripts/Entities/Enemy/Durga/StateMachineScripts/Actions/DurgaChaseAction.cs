using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Durga/Action/Chase")]
public class DurgaChaseAction : Action
{
    public override void Execute(StateMachineController controller)
    {
        Move(controller);

        LookAt(controller);
    }

    void Move(StateMachineController controller)
    {

        controller.agent.destination = controller.offsetTargetPos;
        Vector3 moveTo = controller.transform.forward * (controller.enemyStats.GetStatValue(StatName.Speed) * controller.enemyStats.GetMultValue(MultiplierName.speed)) * controller.deltaTime;

        controller.agent.Move(moveTo);

    }
    bool IsHeadingForCollision(StateMachineController controller)
    {
        if (Vector3.Distance(controller.target.position, controller.transform.position) < 2f)
        {
            Vector3 dir = controller.target.position - controller.transform.position;
            RaycastHit hit;
            if (Physics.SphereCast(controller.transform.position, 1.5f, controller.transform.forward, out hit, 2f, controller.enemyMask))
            {
                return true;
            }
        }
        return false;
    }
    void LookAt(StateMachineController controller)
    {
        Vector3 dir = controller.target.position - controller.transform.position;
        dir.y = 0;
        if (Vector3.Distance(controller.target.position, controller.transform.position) < controller.enemyStats.GetStatValue(StatName.Range))
        {
            Quaternion look = Quaternion.LookRotation(dir);
            controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, look, controller.deltaTime * controller.enemyStats.GetStatValue(StatName.TurnSpeed));

        }
    }

    Vector3 SteerTowards(Vector3 vector, StateMachineController controller)
    {
        Vector3 v = vector.normalized * (controller.enemyStats.GetStatValue(StatName.Speed) * controller.enemyStats.GetMultValue(MultiplierName.speed));
        return Vector3.ClampMagnitude(v, controller.enemyStats.GetStatValue(StatName.Speed) * controller.enemyStats.GetMultValue(MultiplierName.speed));
    }

}
