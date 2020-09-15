using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DestroyObjective", menuName = "Objectives/DestroyObjective")]
public class DestructObjective : Objective
{
    public bool goalDestroyed = false;
    public override void CheckGoal(LevelManager manager)
    {
        if (goalDestroyed)
            manager.SwitchObjective();
    }

    public override void ExecuteObjective(LevelManager manager)
    {

    }
    public void Destroyed()
    {
        goalDestroyed = true;
    }
    public override void ObjEnter(LevelManager manager)
    {
        base.ObjEnter(manager);
        EventSystem.instance.goalDestroyed += Destroyed;
    }

    public override void ObjExit(LevelManager manager)
    {
        base.ObjExit(manager);
        EventSystem.instance.goalDestroyed -= Destroyed;
    }


    private void OnDisable()
    {
        this.started = false;
        this.finished = false;
        goalDestroyed = false;
    }
}
