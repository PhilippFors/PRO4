using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelManager))]
public class RestartArea : Editor
{
    public override void OnInspectorGUI(){
        DrawDefaultInspector();

        LevelManager script = (LevelManager)target;
        if(GUILayout.Button("Restart Area")){
            script.RestartCurrentArea();
        }
    }
}
