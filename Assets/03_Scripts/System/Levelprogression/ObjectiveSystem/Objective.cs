using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objective : ScriptableObject
{
    public ObjectiveTransition[] transitions;
    public bool started;
    public bool finished;
    public void ObjectiveUpdate(LevelManager manager)
    {
        ExecuteObjective(manager);

        CheckTransitions(manager);
    }

    public abstract void ExecuteObjective(LevelManager manager);

    private void CheckTransitions(LevelManager manager)
    {
        foreach (ObjectiveTransition transition in transitions)
        {
            if (transition.decision.Execute(manager))
            {
                manager.SwitchObjective();
            }
        }
    }
    public abstract void StateExit(LevelManager manager);

    public abstract void StateEnter(LevelManager manager);
}
