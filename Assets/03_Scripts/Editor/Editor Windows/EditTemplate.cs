using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Author("Philipp Forstner")]
public class EditTemplate : EditorWindow
{
    int index;
    string[] options;
    bool isStat = true;
    bool isMult = true;
    string templateFolder;
    StatTemplate asset;
    MultVariable m;
    StatVariable v;
    string _name;
    StatName statName;
    StatName deletionStatName;
    MultiplierName multName;
    MultiplierName deletionMultName;
    string[] path;
    bool choosen = false;
    float statfloatField;

    List<StatTemplate> list;
    private void OnGUI()
    {

        GUILayout.Label("Choose a StatTemplate in your Asset folder and then click the button below", EditorStyles.boldLabel);
        if (GUILayout.Button("Choose Template", GUILayout.Height(30f)))
        {
            choosen = false;
        }
        if (!choosen)
        {
            list = new List<StatTemplate>();
            string[] guid = Selection.assetGUIDs;

            path = new string[guid.Length];


            for (int i = 0; i < guid.Length; i++)
            {
                path[i] = AssetDatabase.GUIDToAssetPath(guid[i]);
                if (path[i].Contains("Igner"))
                {
                    templateFolder = "Enemy/Igner";
                }
                else if (path[i].Contains("Durga"))
                {
                    templateFolder = "Enemy/Durga";
                }
                else if (path[i].Contains("Shirugi"))
                {
                    templateFolder = "Enemy/Shirugi";
                }
                else if (path[i].Contains("Player"))
                {
                    templateFolder = "Player";
                }
            }


            for (int i = 0; i < guid.Length; i++)
                list.Add((StatTemplate)AssetDatabase.LoadAssetAtPath(path[i], typeof(StatTemplate)));

            choosen = true;
        }


        asset = (StatTemplate)EditorGUILayout.ObjectField(asset, typeof(StatTemplate), false, GUILayout.MinWidth(200f), GUILayout.MaxWidth(270f));
        // string p = AssetDatabase.GetAssetPath(asset);
        // if (p.Contains("Igner"))
        // {
        //     templateFolder = "Enemy/Igner";
        // }
        // else if (p.Contains("Durga"))
        // {
        //     templateFolder = "Enemy/Durga";
        // }
        // else if (p.Contains("Shirugi"))
        // {
        //     templateFolder = "Enemy/Shirugi";
        // }
        // else if (p.Contains("Player"))
        // {
        //     templateFolder = "Player";
        // }

        foreach (StatTemplate t in list)
        {
            asset = t;
            if (asset != null)
                if (asset.statList != null && asset.statList.Count != 0)
                {
                    foreach (FloatReference f in asset.statList)
                    {
                        if (f.Variable is MultVariable)
                        {
                            isStat = false;
                            isMult = true;
                            MultVariable m = (MultVariable)f.Variable;
                            GUILayout.BeginHorizontal(GUILayout.MinWidth(600f), GUILayout.MaxWidth(400f));
                            f.Variable.Value = EditorGUILayout.FloatField("Mult: " + m.multiplierName.ToString(), m.Value, GUILayout.MinWidth(100f), GUILayout.MaxWidth(200f));
                            EditorGUILayout.ObjectField(m, typeof(MultVariable), GUILayout.MinWidth(150f), GUILayout.MaxWidth(200f));
                            GUILayout.EndHorizontal();
                        }
                        if (f.Variable is StatVariable)
                        {
                            isStat = true;
                            isMult = false;
                            StatVariable s = (StatVariable)f.Variable;
                            GUILayout.BeginHorizontal(GUILayout.MinWidth(600f), GUILayout.MaxWidth(400f));
                            f.Variable.Value = EditorGUILayout.FloatField("Stat: " + s.statName.ToString(), s.Value, GUILayout.MinWidth(100f), GUILayout.MaxWidth(200f));
                            EditorGUILayout.ObjectField(s, typeof(StatVariable), GUILayout.MinWidth(150f), GUILayout.MaxWidth(200f));
                            GUILayout.EndHorizontal();
                        }
                    }
                }
        }

        GUILayout.Space(5f);

        if (isStat)
        {
            GUILayout.Label("Add new Stat", EditorStyles.boldLabel);
            statfloatField = EditorGUILayout.FloatField("Value: ", statfloatField);
            _name = EditorGUILayout.TextField("Name: ", _name);
            statName = (StatName)EditorGUILayout.EnumPopup(statName);
            if (GUILayout.Button("Add Stat"))
            {
                AddStat();

            }
            if (asset != null)
                if (asset.statList.Count != 0)
                {
                    int i = 0;
                    options = new string[asset.statList.Count];
                    foreach (FloatReference f in asset.statList)
                    {
                        StatVariable s = (StatVariable)f.Variable;
                        options[i] = s.name;
                        i++;
                    }

                    GUILayout.Space(5f);
                    GUILayout.Label("Choose Stat to delete", EditorStyles.boldLabel);
                    // deletionStatName = (StatName)EditorGUILayout.EnumPopup(deletionStatName);
                    index = EditorGUILayout.Popup(index, options);
                    if (GUILayout.Button("Delete Stat"))
                    {
                        DeleteStat(index);
                    }
                }


        }
        GUILayout.Space(5f);
        if (isMult)
        {
            GUILayout.Label("Add new Multiplier", EditorStyles.boldLabel);
            statfloatField = EditorGUILayout.FloatField("Value: ", statfloatField);
            _name = EditorGUILayout.TextField("Name: ", _name);
            multName = (MultiplierName)EditorGUILayout.EnumPopup(multName);

            if (GUILayout.Button("Add Mult"))
            {
                AddMult();
            }

            if (asset != null)
                if (asset.statList.Count != 0)
                {
                    int i = 0;
                    options = new string[asset.statList.Count];
                    foreach (FloatReference f in asset.statList)
                    {
                        MultVariable s = (MultVariable)f.Variable;
                        options[i] = s.name;
                        i++;
                    }
                    GUILayout.Space(5f);
                    GUILayout.Label("Choose Multiplier to delete", EditorStyles.boldLabel);
                    // deletionMultName = (MultiplierName)EditorGUILayout.EnumPopup(deletionMultName);
                    index = EditorGUILayout.Popup(index, options);
                    if (GUILayout.Button("Delete Multiplier"))
                    {
                        DeleteMult(index);
                    }
                }

        }
    }

    void DeleteStat(int index)
    {
        foreach (FloatReference f in asset.statList)
        {
            StatVariable s = (StatVariable)f.Variable;
            if (options[index] == s.name)
            {
                asset.statList.Remove(f);
                DeleteAsset(f.Variable.name);
                // DestroyImmediate(f.Variable, true);
            }
        }
    }
    void DeleteMult(int index)
    {
        foreach (FloatReference f in asset.statList)
        {
            MultVariable s = (MultVariable)f.Variable;
            if (options[index] == s.name)
            {
                asset.statList.Remove(f);
                DeleteAsset(f.Variable.name);
            }
        }
    }

    void DeleteAsset(string name)
    {
        string[] str = AssetDatabase.FindAssets(name);
        AssetDatabase.DeleteAsset(AssetDatabase.GUIDToAssetPath(str[0]));
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
        string[] paths = AssetDatabase.FindAssets("t:MultVariable");
        string[] stuff = new string[paths.Length];
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
        _name = null;
        m.multiplierName = multName;
        m.Value = statfloatField;
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
        string[] paths = AssetDatabase.FindAssets("t:StatVariable");
        string[] stuff = new string[paths.Length];
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
        _name = null;

        v.statName = statName;
        v.Value = statfloatField;
        asset.Add(v);
    }

    private void OnInspectorUpdate()
    {
        Repaint();
    }
}
