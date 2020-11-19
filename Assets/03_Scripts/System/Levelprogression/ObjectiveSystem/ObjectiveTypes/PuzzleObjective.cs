using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Author(mainAuthor = "Philipp Forstner")]
[CreateAssetMenu(fileName = "New PuzzleObjective", menuName = "Objectives/PuzzleObjective")]
public class PuzzleObjective : Objective
{
    public override void CheckGoal(LevelManager manager)
    {
        if (letAreaFinish)
            manager.SwitchObjective();
    }

    public override void ExecuteObjective(LevelManager manager)
    {

    }


}
