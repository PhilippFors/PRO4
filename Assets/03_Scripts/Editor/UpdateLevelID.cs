using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(SpawnpointID))]
public class UpdateLevelID : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SpawnpointID script = (SpawnpointID)target;
        if (GUILayout.Button("Update ID's"))
        {
            script.UpdateID();
        }
    }
}
