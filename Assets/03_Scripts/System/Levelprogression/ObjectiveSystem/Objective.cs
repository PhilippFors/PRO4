using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objective : ScriptableObject
{
    public bool started;
    public bool finished;
    public void ObjectiveUpdate(LevelManager manager)
    {
        ExecuteObjective(manager);

        CheckTransitions(manager);
    }

    public abstract void ExecuteObjective(LevelManager manager);

    public abstract void CheckTransitions(LevelManager manager);
    public abstract void ObjExit(LevelManager manager);

    public abstract void ObjEnter(LevelManager manager);

}
