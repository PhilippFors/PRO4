using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneLevelData))]
public class SaveAsset : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SceneLevelData script = (SceneLevelData)target;
        if (GUILayout.Button("Save Asset"))
        {
            script.SaveAsset();
        }
        if (GUILayout.Button("Reload"))
        {
            script.Reload();
        }
    }
}
