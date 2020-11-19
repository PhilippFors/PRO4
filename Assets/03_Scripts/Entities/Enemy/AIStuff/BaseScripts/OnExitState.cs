using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author("Philipp Forstner")]
public abstract class OnExitState : ScriptableObject
{
   public abstract void Execute(StateMachineController controller);
}
