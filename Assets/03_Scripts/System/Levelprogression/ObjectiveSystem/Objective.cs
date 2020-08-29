using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objective : ScriptableObject
{
    public bool started;
    public bool finished;
    public int AreaID;
    public void ObjectiveUpdate(LevelManager manager)
    {
        ExecuteObjective(manager);

        CheckGoal(manager);
    }
    public abstract void ExecuteObjective(LevelManager manager);
    public abstract void CheckGoal(LevelManager manager);
    public abstract void ObjExit(LevelManager manager);
    public abstract void ObjEnter(LevelManager manager);
}
