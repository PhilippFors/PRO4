using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StateEditor : EditorWindow
{
    string[] folderPath;
    string[] paths;
    Vector2 scrollPosition = Vector2.zero;
    List<State> states = new List<State>();

    float maxWidth = 320f;

    bool choosen = true;

    [MenuItem("Tools/Custom Window/State Editor")]
    public static void ShowWindow()
    {
        GetWindow<StateEditor>("State Editor");
    }
    private void OnGUI()
    {
        GUILayout.Label("Choose a Folder in your Asset folder and then click the button below", EditorStyles.boldLabel);
        if (GUILayout.Button("Choose Folder", GUILayout.Height(30f)))
        {
            choosen = false;
        }

        if (!choosen)
        {
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
                states.Add((State)AssetDatabase.LoadAssetAtPath(paths[i], typeof(State)));
            choosen = true;
        }

        if (states.Count == 0)
            return;
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true);
        foreach (State t in states)
        {
            GUILayout.Label(t.name, EditorStyles.largeLabel);
            EditorGUILayout.ObjectField(t, typeof(State), false, GUILayout.MinWidth(200f), GUILayout.MaxWidth(maxWidth));
            EditorGUILayout.Separator();
            var so = new SerializedObject(t);


            int actnr = 0;
            for (int i = 0; i < t.actions.Length; i++)
            {
                GUILayout.Label("Actions ", EditorStyles.boldLabel);
                var ab = so.FindProperty("actions").GetArrayElementAtIndex(i).objectReferenceValue = EditorGUILayout.ObjectField("Action " + actnr, t.actions[i], typeof(Action), false, GUILayout.MinWidth(200f), GUILayout.MaxWidth(maxWidth));
                so.ApplyModifiedProperties();
                actnr++;
            }

            GUILayout.Label("____________", EditorStyles.boldLabel);

            int trnr = 0;
            for (int i = 0; i < t.transitions.Length; i++)
            {
                var tra = so.FindProperty("transitions").GetArrayElementAtIndex(i);
                GUILayout.Label("Transition " + trnr, EditorStyles.boldLabel);
                var d = tra.FindPropertyRelative("decision").objectReferenceValue = EditorGUILayout.ObjectField("Decision", t.transitions[i].decision, typeof(Decision), false, GUILayout.MinWidth(200f), GUILayout.MaxWidth(maxWidth));
                EditorGUILayout.Separator();
                var s = tra.FindPropertyRelative("trueState").objectReferenceValue = EditorGUILayout.ObjectField("True State", t.transitions[i].trueState, typeof(State), false, GUILayout.MinWidth(200f), GUILayout.MaxWidth(maxWidth));
                var f = tra.FindPropertyRelative("falseState").objectReferenceValue = EditorGUILayout.ObjectField("False State", t.transitions[i].falseState, typeof(State), false, GUILayout.MinWidth(200f), GUILayout.MaxWidth(maxWidth));
                so.ApplyModifiedProperties();
                GUILayout.Label("____________", EditorStyles.boldLabel);

                trnr++;
            }

            so.FindProperty("onExitState").objectReferenceValue = EditorGUILayout.ObjectField("On Exit", t.onExitState, typeof(OnExitState), false, GUILayout.MinWidth(200f), GUILayout.MaxWidth(maxWidth));
            so.FindProperty("onEnterState").objectReferenceValue = EditorGUILayout.ObjectField("On Enter", t.onEnterState, typeof(OnEnterState), false, GUILayout.MinWidth(200f), GUILayout.MaxWidth(maxWidth));

            GUILayout.Label("____________", EditorStyles.boldLabel);
            EditorGUILayout.Separator();
            so.ApplyModifiedProperties();
        }

        GUILayout.EndScrollView();
    }
    private void OnInspectorUpdate()
    {
        Repaint();
    }
}
