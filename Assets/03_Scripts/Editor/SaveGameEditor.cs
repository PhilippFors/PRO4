using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveManager))]
public class SaveGameEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SaveManager script = (SaveManager)target;
        if (GUILayout.Button("Save Game"))
        {
            script.Save();
        }
        if (GUILayout.Button("Load Game"))
        {
            script.LoadPlayer();
            script.LoadLevel();
        }
    }

}
