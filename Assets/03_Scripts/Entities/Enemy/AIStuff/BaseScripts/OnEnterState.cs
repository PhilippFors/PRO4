using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author("Philipp Forstner")]
public abstract class OnEnterState : ScriptableObject
{
    public abstract void Execute(StateMachineController controller);
}
