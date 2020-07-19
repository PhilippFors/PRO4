using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Avik/Action/Attack")]
public class AvikAttackAction : Action
{
    public override void Execute(StateMachineController controller){
        
        Attack(controller);
    }

    private void Attack(StateMachineController controller)
    {
        Debug.Log("He attack");
    }


}
