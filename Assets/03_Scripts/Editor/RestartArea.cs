using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OLD_LevelManager))]
public class RestartArea : Editor
{
    public override void OnInspectorGUI(){
        DrawDefaultInspector();

        OLD_LevelManager script = (OLD_LevelManager)target;
        if(GUILayout.Button("Restart Area")){
            script.RestartCurrentArea();
        }
    }
}
