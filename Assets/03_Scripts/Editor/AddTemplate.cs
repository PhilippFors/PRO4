using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class AddTemplate : EditorWindow
{
    string[] paths;
    string[] stuff;
    float statfloatField;
    string name;
    string templatename;
    string templateFolder = "Enemy";

    StatName statName;
    MultiplierName multName;
    List<StatTemplate> templates = new List<StatTemplate>();
    StatTemplate asset;
    StatVariable v;

    MultVariable m;
    bool init = false;
    bool statFoldout = false;
    bool multFoldout = false;

    bool statAdded = false;
    bool multAdded = false;

    bool toggle = false;
    public static void ShowWindow()
    {
        GetWindow<AddTemplate>("Add Template");
    }
    private void OnGUI()
    {

        if (!init)
        {
            templatename = EditorGUILayout.TextField("Template Name: ", templatename);

            GUILayout.Label("Toggle target folder");
            toggle = EditorGUILayout.Toggle(toggle);

            if (toggle)
            {
                templateFolder = "Enemy";

            }
            else
            {
                templateFolder = "Player";
            }
            GUILayout.Label("Target Folder is: " + templateFolder);
            if (GUILayout.Button("Save Asset"))
            {
                StatTemplate newasset = ScriptableObject.CreateInstance<StatTemplate>();
                if (templatename == null)
                {
                    templatename = "NewStatTemplate";
                }
                int n = 1;

                paths = AssetDatabase.FindAssets("t:StatTemplate");
                stuff = new string[paths.Length];
                for (int i = 0; i < paths.Length; i++)
                {
                    stuff[i] = AssetDatabase.GUIDToAssetPath(paths[i]);
                }
                for (int i = 0; i < paths.Length; i++)
                    templates.Add((StatTemplate)AssetDatabase.LoadAssetAtPath(stuff[i], typeof(StatTemplate)));

                foreach (StatTemplate t in templates)
                {
                    if (t.name.Equals(templatename))
                    {
                        templatename = "NewStatObject" + n.ToString();
                        n++;
                    }
                }
                AssetDatabase.CreateAsset(newasset, "Assets/03_Scripts/Entities/StatTemplates/" + templateFolder + "/" + templatename + ".asset");
                AssetDatabase.SaveAssets();
                init = true;

                templates.Clear();

                paths = AssetDatabase.FindAssets("t:StatTemplate");
                stuff = new string[paths.Length];
                for (int i = 0; i < paths.Length; i++)
                {
                    stuff[i] = AssetDatabase.GUIDToAssetPath(paths[i]);

                }
                for (int i = 0; i < paths.Length; i++)
                    templates.Add((StatTemplate)AssetDatabase.LoadAssetAtPath(stuff[i], typeof(StatTemplate)));

                foreach (StatTemplate t in templates)
                {
                    if (t.name.Equals(templatename))
                    {
                        asset = t;
                    }
                }
            }
        }

        if (init)
        {
            EditorGUILayout.ObjectField(asset, typeof(StatTemplate), GUILayout.MinWidth(200f), GUILayout.MaxWidth(270f));
            if (asset.statList != null && asset.statList.Count != 0)
            {
                foreach (FloatReference f in asset.statList)
                {
                    if (f.Variable is MultVariable)
                    {
                        MultVariable m = (MultVariable)f.Variable;
                        
                        f.Variable.Value = EditorGUILayout.FloatField("Mult: " + m.multiplierName.ToString(), m.Value, GUILayout.MinWidth(100f), GUILayout.MaxWidth(200f));
                        
                        
                    }
                    if (f.Variable is StatVariable)
                    {
                        StatVariable s = (StatVariable)f.Variable;
                        f.Variable.Value = EditorGUILayout.FloatField("Stat: " + s.statName.ToString(), s.Value, GUILayout.MinWidth(100f), GUILayout.MaxWidth(200f));
                    }
                }
            }


            GUILayout.Label("Values for Stat/Mult");
            statfloatField = EditorGUILayout.FloatField("Value: ", statfloatField);
            name = EditorGUILayout.TextField("Name: ", name);
            GUILayout.Space(5f);
            if (!multAdded)
            {
                statFoldout = EditorGUILayout.Foldout(statFoldout, "Add Stat");
                if (statFoldout)
                {
                    statName = (StatName)EditorGUILayout.EnumPopup(statName);
                    if (GUILayout.Button("Add Stat"))
                    {
                        AddStat();
                        statAdded = true;
                    }
                }
            }
            GUILayout.Space(5f);
            if (!statAdded)
            {
                multFoldout = EditorGUILayout.Foldout(multFoldout, "Add Mult");
                if (multFoldout)
                {
                    multName = (MultiplierName)EditorGUILayout.EnumPopup(multName);

                    if (GUILayout.Button("Add Mult"))
                    {
                        AddMult();
                        multAdded = true;
                    }
                }
            }
        }

    }


    void AddMult()
    {
        m = ScriptableObject.CreateInstance<MultVariable>();
        int n = 1;
        List<MultVariable> list = new List<MultVariable>();
        if (name == null)
        {
            name = "NewMultVariable";
        }
        paths = AssetDatabase.FindAssets("t:MultVariable");
        stuff = new string[paths.Length];
        for (int i = 0; i < paths.Length; i++)
        {
            stuff[i] = AssetDatabase.GUIDToAssetPath(paths[i]);
        }
        for (int i = 0; i < paths.Length; i++)
            list.Add((MultVariable)AssetDatabase.LoadAssetAtPath(stuff[i], typeof(MultVariable)));

        foreach (MultVariable t in list)
        {
            if (t.name.Equals(name))
            {
                name = name + n.ToString();
                n++;
            }
        }
        AssetDatabase.CreateAsset(v, "Assets/03_Scripts/Entities/StatTemplates/" + templateFolder + "/Mults/" + name + ".asset");
        AssetDatabase.SaveAssets();


        paths = AssetDatabase.FindAssets("t:MultVariable");
        stuff = new string[paths.Length];
        list.Clear();
        for (int i = 0; i < paths.Length; i++)
        {
            stuff[i] = AssetDatabase.GUIDToAssetPath(paths[i]);
        }
        for (int i = 0; i < paths.Length; i++)
            list.Add((MultVariable)AssetDatabase.LoadAssetAtPath(stuff[i], typeof(MultVariable)));

        foreach (MultVariable t in list)
        {
            if (t.name.Equals(name))
            {
                m = t;
            }
        }

        m.multiplierName = multName;
        m.Value = statfloatField;
        asset.Add(v);
    }
    void AddStat()
    {
        v = ScriptableObject.CreateInstance<StatVariable>();
        int n = 1;
        List<StatVariable> list = new List<StatVariable>();
        if (name == null)
        {
            name = "NewStatVariable";
        }
        paths = AssetDatabase.FindAssets("t:StatVariable");
        stuff = new string[paths.Length];
        for (int i = 0; i < paths.Length; i++)
        {
            stuff[i] = AssetDatabase.GUIDToAssetPath(paths[i]);
        }
        for (int i = 0; i < paths.Length; i++)
            list.Add((StatVariable)AssetDatabase.LoadAssetAtPath(stuff[i], typeof(StatVariable)));

        foreach (StatVariable t in list)
        {
            if (t.name.Equals(name))
            {
                name = name + n.ToString();
                n++;
            }
        }
        AssetDatabase.CreateAsset(v, "Assets/03_Scripts/Entities/StatTemplates/" + templateFolder + "/Stats/" + name + ".asset");
        AssetDatabase.SaveAssets();


        paths = AssetDatabase.FindAssets("t:StatVariable");
        stuff = new string[paths.Length];
        list.Clear();
        for (int i = 0; i < paths.Length; i++)
        {
            stuff[i] = AssetDatabase.GUIDToAssetPath(paths[i]);
        }
        for (int i = 0; i < paths.Length; i++)
            list.Add((StatVariable)AssetDatabase.LoadAssetAtPath(stuff[i], typeof(StatVariable)));

        foreach (StatVariable t in list)
        {
            if (t.name.Equals(name))
            {
                v = t;
            }
        }

        v.statName = statName;
        v.Value = statfloatField;
        asset.Add(v);
    }
}
