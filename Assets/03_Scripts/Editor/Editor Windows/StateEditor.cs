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

                int actnr = 1;
                GUILayout.Label("Actions ", EditorStyles.boldLabel);
                var ab = so.FindProperty("actions");
                if (t.actions.Length != 0)
                    for (int i = 0; i < t.actions.Length; i++)
                    {

                        var s = ab.GetArrayElementAtIndex(i).objectReferenceValue = EditorGUILayout.ObjectField("Action " + actnr, t.actions[i], typeof(Action), false, GUILayout.MinWidth(200f), GUILayout.MaxWidth(maxWidth));
                        so.ApplyModifiedProperties();
                        actnr++;

                    }

                if (GUILayout.Button("Add Action"))
                {
                    so.FindProperty("actions").InsertArrayElementAtIndex(t.actions.Length);
                    so.ApplyModifiedProperties();
                }
                if (t.actions.Length != 0)
                    if (GUILayout.Button("Remove Action"))
                    {
                        so.FindProperty("actions").GetArrayElementAtIndex(t.actions.Length - 1).objectReferenceValue = null;
                        so.FindProperty("actions").DeleteArrayElementAtIndex(t.actions.Length - 1);
                        so.ApplyModifiedProperties();
                    }
                EditorGUILayout.Separator();

                transitionBools[j] = EditorGUILayout.Foldout(transitionBools[j], "Transitions");
                var tra = so.FindProperty("transitions");
                if (t.transitions.Length != 0)
                {
                    if (transitionBools[j])
                    {
                        int trnr = 1;


                        for (int i = 0; i < t.transitions.Length; i++)
                        {
                            var tr = tra.GetArrayElementAtIndex(i);
                            GUILayout.Label("Transition " + trnr, EditorStyles.boldLabel);
                            var d = tr.FindPropertyRelative("decision").objectReferenceValue = EditorGUILayout.ObjectField("Decision", t.transitions[i].decision, typeof(Decision), false, GUILayout.MinWidth(200f), GUILayout.MaxWidth(maxWidth));
                            var s = tr.FindPropertyRelative("trueState").objectReferenceValue = EditorGUILayout.ObjectField("True State", t.transitions[i].trueState, typeof(State), false, GUILayout.MinWidth(200f), GUILayout.MaxWidth(maxWidth));
                            var f = tr.FindPropertyRelative("falseState").objectReferenceValue = EditorGUILayout.ObjectField("False State", t.transitions[i].falseState, typeof(State), false, GUILayout.MinWidth(200f), GUILayout.MaxWidth(maxWidth));
                            so.ApplyModifiedProperties();
                            EditorGUILayout.Separator();

                            trnr++;
                        }
                    }
                }
                if (GUILayout.Button("Add Transition"))
                {
                    so.FindProperty("transitions").InsertArrayElementAtIndex(t.transitions.Length);
                    so.ApplyModifiedProperties();
                }
                if (t.transitions.Length != 0)
                    if (GUILayout.Button("Remove Transition"))
                    {
                        so.FindProperty("transitions").GetArrayElementAtIndex(t.transitions.Length - 1).objectReferenceValue = null;
                        so.FindProperty("transitions").DeleteArrayElementAtIndex(t.transitions.Length - 1);
                        so.ApplyModifiedProperties();
                    }

                EditorGUILayout.Separator();

                so.FindProperty("onExitState").objectReferenceValue = EditorGUILayout.ObjectField("On Exit", t.onExitState, typeof(OnExitState), false, GUILayout.MinWidth(200f), GUILayout.MaxWidth(maxWidth));
                so.FindProperty("onEnterState").objectReferenceValue = EditorGUILayout.ObjectField("On Enter", t.onEnterState, typeof(OnEnterState), false, GUILayout.MinWidth(200f), GUILayout.MaxWidth(maxWidth));

                EditorGUILayout.Separator();

                so.ApplyModifiedProperties();
            }
            j++;
        }

        GUILayout.EndScrollView();


        GUILayout.EndScrollView();
    }

    private void OnInspectorUpdate()
    {
        Repaint();
    }
}
