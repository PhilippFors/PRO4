using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelSaveData
{
    public int currentLevel;
    public int currentArea;
    
    public bool currentAreaFinsihed;
    public LevelSaveData(LevelManager data){
        currentLevel = data.GetCurrentLevel();
        currentArea = data.GetCurrentArea();

        currentAreaFinsihed = data.levelData[data.GetCurrentLevel()].areas[data.GetCurrentArea()].finished;
    }

}
