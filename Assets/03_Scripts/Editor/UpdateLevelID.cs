using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[Author("Philipp Forstner")]
[CustomEditor(typeof(SpawnPointWorker))]
public class UpdateLevelID : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SpawnPointWorker script = (SpawnPointWorker)target;
        if (GUILayout.Button("Update ID's"))
        {
            script.UpdateID();
        }
    }
}
