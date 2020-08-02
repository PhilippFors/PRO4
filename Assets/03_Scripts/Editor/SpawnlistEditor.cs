using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnpointList))]
public class SpawnlistEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SpawnpointList script = (SpawnpointList)target;
        if (GUILayout.Button("Find Spawnpoints"))
        {
            script.FindSpawnpoints();
        }

    }
}
