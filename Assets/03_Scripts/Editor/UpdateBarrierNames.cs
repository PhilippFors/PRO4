using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Author("Philipp Forstner")]
[CustomEditor(typeof(AreaBarrier))]
public class UpdateBarrierNames : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AreaBarrier script = (AreaBarrier)target;
        if (GUILayout.Button("Update names"))
        {
            script.UdpateNames();
        }
    }
}
