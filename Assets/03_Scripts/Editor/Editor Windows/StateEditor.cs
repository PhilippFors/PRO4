using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StateEditor : EditorWindow
{
    string[] folderPath = new string[2];
    string[] paths;
    Vector2 scrollPosition = Vector2.zero;
    List<State> states = new List<State>();
    List<bool> stateBools = new List<bool>();
    List<bool> transitionBools = new List<bool>();
    float maxWidth = 320f;
    bool stateCollapse;
    bool choosen = true;
    [MenuItem("Tools/Custom Window/State Editor")]
    public static void ShowWindow()
    {
        GetWindow<StateEditor>("State Editor");
    }
    private void OnGUI()
    {
        GUILayout.Label("Choose a Folder filled with SO States in your Asset folder and then click the button below", EditorStyles.boldLabel);
        if (GUILayout.Button("Choose Folder", GUILayout.Height(30f)))
        {
            choosen = false;
        }

        if (!choosen)
        {
            stateBools = new List<bool>();
            states = new List<State>();
            string[] guid = Selection.assetGUIDs;
            folderPath = new string[guid.Length];
            for (int i = 0; i < guid.Length; i++)
                folderPath[i] = AssetDatabase.GUIDToAssetPath(guid[i]);

            paths = AssetDatabase.FindAssets("t:State", folderPath);

            for (int i = 0; i < paths.Length; i++)
            {
                paths[i] = AssetDatabase.GUIDToAssetPath(paths[i]);
            }

            for (int i = 0; i < paths.Length; i++)
            {
                states.Add((State)AssetDatabase.LoadAssetAtPath(paths[i], typeof(State)));
                stateBools.Add(false);
                transitionBools.Add(true);
            }
            choosen = true;

        }
        GUILayout.Label(folderPath[0]);
        if (states.Count == 0 || states == null)
            return;
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true);
        int j = 0;
        foreach (State t in states)
        {
            stateBools[j] = EditorGUILayout.Foldout(stateBools[j], t.name);
            if (stateBools[j])
            {
                EditorGUILayout.ObjectField(t, typeof(State), false, GUILayout.MinWidth(200f), GUILayout.MaxWidth(maxWidth));
                EditorGUILayout.Separator();
                var so = new SerializedObject(t);


                GUILayout.Label("Actions ", EditorStyles.boldLabel);
                var ab = so.FindProperty("actions");
            }

            GUILayout.EndScrollView();
        }
    }
    private void OnInspectorUpdate()
    {
        Repaint();
    }
}
