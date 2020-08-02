using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
[ExecuteInEditMode]
public class SceneLevelData : MonoBehaviour
{
    public Level levelInfo;
    public void OnEnable()
    {
        if (levelInfo == null)
            levelInfo = ScriptableObject.CreateInstance<Level>();
    }

    public void SaveAsset()
    {
        AssetDatabase.CreateAsset(levelInfo, "Assets/03_Scripts/" + gameObject.name +".asset");
        var so = new SerializedObject(levelInfo);
        so.ApplyModifiedProperties();
        AssetDatabase.SaveAssets();
    }

    public void Reload()
    {
        if (levelInfo == null)
            levelInfo = ScriptableObject.CreateInstance<Level>();
    }


}
