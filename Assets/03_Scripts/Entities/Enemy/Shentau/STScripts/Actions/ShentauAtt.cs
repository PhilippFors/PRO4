using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Shentau/Action/Attack")]
public class ShentauAtt : Action
{
    public override void Execute(StateMachineController controller)
    {
        Vector3 dir = controller.settings.playerTarget.position - controller.transform.position;
        Quaternion look = Quaternion.LookRotation(dir);
        controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, look, controller.deltaTime * controller.enemyStats.GetStatValue(StatName.TurnSpeed));
        RaycastHit hit;
        if (Physics.Raycast(controller.transform.position, controller.transform.forward, out hit, 8f, LayerMask.GetMask("Player")))
        {
            if (controller.actions.CheckIsAttacking(controller))
                controller.actions.Attack(controller);
        }

    }
}
