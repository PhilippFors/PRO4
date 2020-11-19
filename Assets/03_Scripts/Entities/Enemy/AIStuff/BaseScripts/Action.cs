using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Author("Philipp Forstner")]
public abstract class Action : ScriptableObject
{
    public abstract void Execute(StateMachineController controller);
}
