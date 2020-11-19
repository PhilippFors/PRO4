using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Author("Philipp Forstner")]
public class AddTemplate : EditorWindow
{
    string[] paths;
    string[] stuff;
    string[] options = { "Ralger", "Avik", "Shentau", "Player" };
    int index;
    float statfloatField;
    string _name;
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

            GUILayout.Label("For which Entity is this Template?");
            index = EditorGUILayout.Popup(index, options);

            switch (index)
            {
                case 0:
                    templateFolder = "Enemy/Igner";
                    break;
                case 1:
                    templateFolder = "Enemy/Avik";
                    break;
                case 2:
                    templateFolder = "Enemy/Shentau";
                    break;
                case 3:
                    templateFolder = "Player";
                    break;
            }

            GUILayout.Label("Target Folder is: 03_Scripts/Entities/StatTemplates/" + templateFolder);
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

                bool yes = false;
                foreach (StatTemplate t in templates)
                {
                    string na = templatename + n.ToString();
                    Debug.Log(t.name);
                    if (t.name.Equals(na))
                    {
                        n++;
                    }
                }
                templatename = templatename + n.ToString();
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

            GUILayout.Space(5f);
            if (!multAdded & !statAdded)
                GUILayout.Label("The first thing you add will determine the type of the StatTemplate", EditorStyles.boldLabel);
            if (!multAdded)
            {
                GUILayout.Label("Set values for Stat", EditorStyles.boldLabel);
                statfloatField = EditorGUILayout.FloatField("Value: ", statfloatField);
                _name = EditorGUILayout.TextField("Name: ", _name);

                statName = (StatName)EditorGUILayout.EnumPopup(statName);
                if (GUILayout.Button("Add Stat"))
                {
                    AddStat();
                    statAdded = true;
                }

            }
            GUILayout.Space(5f);
            if (!statAdded)
            {
                GUILayout.Label("Set values for Multiplier", EditorStyles.boldLabel);
                statfloatField = EditorGUILayout.FloatField("Value: ", statfloatField);
                _name = EditorGUILayout.TextField("Name: ", _name);

                multName = (MultiplierName)EditorGUILayout.EnumPopup(multName);

                if (GUILayout.Button("Add Mult"))
                {
                    AddMult();
                    multAdded = true;
                }


            }

        }
    }


    void AddMult()
    {
        m = ScriptableObject.CreateInstance<MultVariable>();
        int n = 1;
        List<MultVariable> list = new List<MultVariable>();
        if (_name == null)
        {
            _name = "NewMultVariable";
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
            string na = _name + n.ToString();
            Debug.Log(t.name);
            if (t.name.Equals(na))
            {
                n++;
            }
        }
        _name = _name + n.ToString();
        AssetDatabase.CreateAsset(m, "Assets/03_Scripts/Entities/StatTemplates/" + templateFolder + "/Mults/" + _name + ".asset");
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
            if (t.name.Equals(_name))
            {
                m = t;
            }
        }

        m.multiplierName = multName;
        m.Value = statfloatField;
        _name = null;
        asset.Add(m);
    }
    void AddStat()
    {
        v = ScriptableObject.CreateInstance<StatVariable>();
        int n = 1;
        List<StatVariable> list = new List<StatVariable>();
        if (_name == null)
        {
            _name = "NewStatVariable";
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
            string na = _name + n.ToString();
            Debug.Log(t.name);
            if (t.name.Equals(na))
            {
                n++;
            }
        }
        _name = _name + n.ToString();

        AssetDatabase.CreateAsset(v, "Assets/03_Scripts/Entities/StatTemplates/" + templateFolder + "/Stats/" + _name + ".asset");
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
            if (t.name.Equals(_name))
            {
                v = t;
            }
        }

        v.statName = statName;
        v.Value = statfloatField;
        _name = null;
        asset.Add(v);
    }
    private void OnInspectorUpdate()
    {
        Repaint();
    }
}
