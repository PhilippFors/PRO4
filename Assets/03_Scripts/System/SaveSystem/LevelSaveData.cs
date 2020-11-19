using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author(mainAuthor = "Philipp Forstner")]
[System.Serializable]
public class LevelSaveData
{
    public int currentLevel;
    public int currentArea;
    
    public bool currentAreaFinsihed;
    public LevelSaveData(LevelManager data){
        currentLevel = data.currentLevel;
        currentArea = data.currentArea;

        currentAreaFinsihed = data.levelData[data.currentLevel].areas[data.currentArea].finished;
    }

}
