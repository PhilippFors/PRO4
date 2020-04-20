using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Durga/Action/Chase")]
public class DurgaChaseAction : Action
{
    public override void Execute(StateMachineController controller)
    {
        Vector3 nextPosition = controller.transform.forward *
        (controller.enemystats.GetStatValue(StatName.speed) *
        controller.enemystats.GetMultValue(MultiplierName.speed)) *
        controller.deltaTime;
        Vector3 dir = controller.target.position - controller.transform.position;
        dir.y = 0;
        Quaternion look = Quaternion.LookRotation(dir);
        controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, look, controller.deltaTime * controller.enemystats.GetStatValue(StatName.turnSpeed));
        controller.agent.SetDestination(controller.target.position);
        // controller.transform.position += controller.transform.forward * controller.enemystats.GetCalculatedValue(StatName.speed, MultiplierName.speed) * Time.deltaTime;
    }
}
