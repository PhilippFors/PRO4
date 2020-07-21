using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Avik/Action/Attack")]
public class AvikAttackAction : Action
{
    public override void Execute(StateMachineController controller)
    {
        if (controller.canAttack)
            Attack(controller);
    }

    private void Attack(StateMachineController controller)
    {
        int one = GetFirstAttack();
        bool two = GetSecondAttack(one, controller);
        controller.actions.Attack(controller, one, two);
    }

    int GetFirstAttack()
    {
        int r = Random.Range(0, 100);
        if (r <= 50)
            return 1;
        else
            return 2;
    }

    bool GetSecondAttack(int one, StateMachineController controller)
    {
        int r = Random.Range(0, 100);
        return r <= controller.settings.comboBias;
    }
}
