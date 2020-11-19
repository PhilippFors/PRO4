using UnityEngine;
using UnityEditor;

[Author("Philipp Forstner")]

[CustomEditor(typeof(LevelManager))]
public class RestartArea : Editor
{
    public override void OnInspectorGUI(){
        DrawDefaultInspector();

        // LevelManager script = (LevelManager)target;
        // if(GUILayout.Button("Restart Area")){
        //     script.RestartCurrentArea();
        // }
    }
}
