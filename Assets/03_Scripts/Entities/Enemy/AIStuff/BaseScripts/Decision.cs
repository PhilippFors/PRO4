using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Author("Philipp Forstner")]
public abstract class Decision : ScriptableObject
{
  public abstract bool Execute(StateMachineController controller);
}
