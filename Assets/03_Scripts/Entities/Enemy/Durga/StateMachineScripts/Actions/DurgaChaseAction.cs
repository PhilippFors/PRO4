using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Durga/Action/Chase")]
public class DurgaChaseAction : Action
{
    public override void Execute(StateMachineController controller)
    {

        Vector3 dir = controller.target.position - controller.transform.position;
        dir.y = 0;

        controller.agent.destination = controller.target.position;
        Vector3 moveTo = controller.transform.forward *
(controller.enemystats.GetStatValue(StatName.Speed) *
controller.enemystats.GetMultValue(MultiplierName.speed)) *
controller.deltaTime;
        controller.nextMovePosition = moveTo;
        controller.agent.Move(moveTo);

        if (Vector3.Distance(controller.target.position, controller.transform.position) < controller.enemystats.GetStatValue(StatName.Range))
        {
            Quaternion look = Quaternion.LookRotation(dir);
            controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, look, controller.deltaTime * controller.enemystats.GetStatValue(StatName.TurnSpeed));

        }
        // controller.transform.position += controller.transform.forward * controller.enemystats.GetCalculatedValue(StatName.speed, MultiplierName.speed) * Time.deltaTime;
    }
}
