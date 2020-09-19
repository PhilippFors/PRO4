using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Shentau/Action/Attack")]
public class ShentauAtt : Action
{
    float delay = 0.2f;
    float countdown = 0f;
    public override void Execute(StateMachineController controller)
    {
        Vector3 dir = controller.aiManager.playerTarget.position - controller.transform.position;
        Quaternion look = Quaternion.LookRotation(dir);
        controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, look, controller.deltaTime * controller.enemyStats.GetStatValue(StatName.TurnSpeed));
        RaycastHit hit;
        if (Physics.Raycast(controller.RayEmitter.position, controller.RayEmitter.forward, out hit, 15f, controller.aiManager.playerMask))
        {
            if (WaitForDelay(controller))
            {
                controller.actions.Attack(controller, 3);
                controller.actions.Attack(controller, 0);
            }
        }
    }

    bool WaitForDelay(StateMachineController c)
    {
        if (countdown >= delay)
        {
            countdown = 0;
            return true;
        }
        countdown += c.deltaTime;
        return false;
    }
}
