using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SOTemplateEditor : EditorWindow
{
    bool init = false;
    int pathlength;
    int oldPathlength;
    Vector2 scrollPosition = Vector2.zero;
    List<StatTemplate> templates = new List<StatTemplate>();
    string[] paths;
    string[] stuff;
    [MenuItem("Tools/Custom Window/Template Editor")]
    public static void ShowWindow()
    {
        GetWindow<SOTemplateEditor>("Template Editor");
    }

    void OnGUI()
    {
        if (GUILayout.Button("RELOAD"))
        {
            templates.Clear();
            init = false;
        }

        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true);
        paths = AssetDatabase.FindAssets("t:StatTemplate");
        pathlength = paths.Length;

        if (pathlength != oldPathlength)
        {
            templates.Clear();
            init = false;
            oldPathlength = pathlength;
        }
        stuff = new string[paths.Length];
        for (int i = 0; i < paths.Length; i++)
        {
            stuff[i] = AssetDatabase.GUIDToAssetPath(paths[i]);
        }

        if (!init)
        {
            oldPathlength = pathlength;
            for (int i = 0; i < paths.Length; i++)
                templates.Add((StatTemplate)AssetDatabase.LoadAssetAtPath(stuff[i], typeof(StatTemplate)));
            init = true;
        }


        foreach (StatTemplate t in templates)
        {
            EditorGUILayout.ObjectField(t, typeof(StatTemplate), GUILayout.MinWidth(200f), GUILayout.MaxWidth(270f));
            if (t.statList != null && t.statList.Count != 0)
            {
                foreach (FloatReference f in t.statList)
                {
                    if (f.Variable is MultVariable)
                    {
                        MultVariable m = (MultVariable)f.Variable;
                        GUILayout.BeginHorizontal(GUILayout.MinWidth(600f), GUILayout.MaxWidth(400f));
                        f.Variable.Value = EditorGUILayout.FloatField("Mult: " + m.multiplierName.ToString(), m.Value, GUILayout.MinWidth(100f), GUILayout.MaxWidth(200f));
                        EditorGUILayout.ObjectField(m, typeof(MultVariable), GUILayout.MinWidth(150f), GUILayout.MaxWidth(200f));
                        GUILayout.EndHorizontal();
                    }
                    if (f.Variable is StatVariable)
                    {
                        StatVariable s = (StatVariable)f.Variable;
                        GUILayout.BeginHorizontal(GUILayout.MinWidth(600f), GUILayout.MaxWidth(400f));
                        f.Variable.Value = EditorGUILayout.FloatField("Stat: " + s.statName.ToString(), s.Value, GUILayout.MinWidth(100f), GUILayout.MaxWidth(200f));
                        EditorGUILayout.ObjectField(s, typeof(StatVariable), GUILayout.MinWidth(150f), GUILayout.MaxWidth(200f));
                        GUILayout.EndHorizontal();
                    }
                }
            }
            GUILayout.Space(1f);
            GUILayout.Label("____________", EditorStyles.boldLabel);
            GUILayout.Space(1f);
        }

        if (GUILayout.Button("Create new template"))
        {
            GetWindow<AddTemplate>("Add Template");
        }

        GUILayout.EndScrollView();
    }

}
