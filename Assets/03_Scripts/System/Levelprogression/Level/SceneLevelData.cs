using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Author(mainAuthor = "Philipp Forstner")]
public class SceneLevelData : MonoBehaviour
{
    public LevelData levelInfo;
    public void OnEnable()
    {
        if (levelInfo == null)
            levelInfo = ScriptableObject.CreateInstance<LevelData>();
    }

    // public void SaveAsset()
    // {
    //     AssetDatabase.CreateAsset(levelInfo, "Assets/03_Scripts/" + gameObject.name +".asset");
    //     var so = new SerializedObject(levelInfo);
    //     so.ApplyModifiedProperties();
    //     AssetDatabase.SaveAssets();
    // }

    public void Reload()
    {
        if (levelInfo == null)
            levelInfo = ScriptableObject.CreateInstance<LevelData>();
    }


}
