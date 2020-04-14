using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Durga/Decision/IdleDecision")]
public class GetOutofIdleDecision : Decision
{
  public override bool ExecuteDecision(StateMachineController controller){
      if(controller.target != null){
          return true;
      }
      return false;
  }
}
