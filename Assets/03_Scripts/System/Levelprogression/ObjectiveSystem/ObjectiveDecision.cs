using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectiveDecision : ScriptableObject
{
    public abstract bool Execute(LevelManager manager);
}
