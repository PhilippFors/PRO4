using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author("Philipp Forstner")]
[CreateAssetMenu(menuName = "PluggableAI/Avik/Action/Chase")]
public class AvikChaseAction : Action
{
    public override void Execute(StateMachineController controller)
    {
        Move(controller);

        LookAt(controller);
    }

    void Move(StateMachineController controller)
    {
        if (Vector3.Distance(controller.aiManager.playerTarget.position, controller.transform.position) < 5f)
            controller.agent.destination = controller.aiManager.playerTarget.position;
        else
            controller.agent.destination = controller.offsetTargetPos;
            
        Vector3 moveTo = controller.transform.forward * (controller.enemyStats.GetStatValue(StatName.Speed) * controller.enemyStats.GetMultValue(MultiplierName.speed)) * controller.deltaTime;

        controller.agent.Move(moveTo);

    }
    void LookAt(StateMachineController controller)
    {
        Vector3 dir = controller.aiManager.playerTarget.position - controller.transform.position;
        dir.y = 0;
        if (Vector3.Distance(controller.aiManager.playerTarget.position, controller.transform.position) < 3f)
        {
            Quaternion look = Quaternion.LookRotation(dir);
            controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, look, controller.deltaTime * controller.enemyStats.GetStatValue(StatName.TurnSpeed));
        }
    }
}
