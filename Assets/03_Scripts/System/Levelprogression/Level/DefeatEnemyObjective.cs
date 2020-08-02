using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DefeatEnemyObjective", menuName = "Objectives/DefeatEnemyObjective")]
public class DefeatEnemyObjective : Objective
{
    public override void ExecuteObjective(LevelManager manager)
    {
        Debug.Log("This Objective is executet: " + name);
    }

    public override void StateEnter(LevelManager manager)
    {
        Debug.Log("State Enter");
    }

    public override void StateExit(LevelManager manager)
    {
        Debug.Log("State Exit");
    }

}
