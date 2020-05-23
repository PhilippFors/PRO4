using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SOTemplateEditor : EditorWindow
{
    bool init = false;
    List<StatTemplate> templates = new List<StatTemplate>();
    string[] paths;
    string[] stuff = new string[10];
    [MenuItem("Tools/Custom Window/Template Editor")]
    public static void ShowWindow()
    {
        GetWindow<SOTemplateEditor>("Template Editor");
    }

    void OnGUI()
    {

        paths = AssetDatabase.FindAssets("t:StatTemplate");
        for (int i = 0; i < paths.Length; i++)
        {
            stuff[i] = AssetDatabase.GUIDToAssetPath(paths[i]);
        }

        if (!init)
        {
            for (int i = 0; i < paths.Length; i++)
                templates.Add((StatTemplate)AssetDatabase.LoadAssetAtPath(stuff[i], typeof(StatTemplate)));
            init = true;
        }


        foreach (StatTemplate t in templates)
            EditorGUILayout.ObjectField(t, typeof(StatTemplate));


    }

}
