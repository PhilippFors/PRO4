using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class SpawnpointEditor : EditorWindow
{
    bool choosen = true;
    float maxWidth = 400f;
    List<LevelData> list = new List<LevelData>();
    string[] path;
    List<bool> areaBools = new List<bool>();
    List<bool> waveBools = new List<bool>();
    List<bool> spawnBools = new List<bool>();
    bool areaBool = false;
    bool waveBool = false;
    bool spawnBool = false;
    [MenuItem("Tools/Custom Window/SpawnPoint Editor")]
    public static void ShowWindow()
    {
        GetWindow<SpawnpointEditor>("SpawnPoint Editor");
    }

    void OnGUI()
    {
        //     GUILayout.Label("Choose a Level in your Asset folder and then click the button below", EditorStyles.boldLabel);
        //     if (GUILayout.Button("Choose Level", GUILayout.Height(30f)))
        //     {
        //         choosen = false;
        //     }
        //     if (!choosen)
        //     {
        //         areaBools = new List<bool>();
        //         waveBools = new List<bool>();
        //         spawnBools = new List<bool>();
        //         list = new List<Level>();
        //         string[] guid = Selection.assetGUIDs;

        //         path = new string[guid.Length];
        //         for (int i = 0; i < guid.Length; i++)
        //         {
        //             path[i] = AssetDatabase.GUIDToAssetPath(guid[i]);
        //         }
        //         for (int i = 0; i < guid.Length; i++)
        //             list.Add((Level)AssetDatabase.LoadAssetAtPath(path[i], typeof(Level)));

        //         foreach (Level level in list)
        //         {
        //             for (int i = 0; i < level.areas.Length; i++)
        //             {
        //                 areaBools.Add(false);
        //                 for (int j = 0; j < level.areas[i].waves.Length; j++)
        //                 {
        //                     waveBools.Add(false);
        //                     for (int x = 0; x < level.areas[i].waves[j].spawnPoints.Length; x++)
        //                     {
        //                         spawnBools.Add(false);
        //                     }
        //                 }
        //             }

        //         }
        //         choosen = true;
        //     }

        //     if (list.Count != 0)
        //         foreach (Level level in list)
        //         {
        //             GUILayout.Label("Level", EditorStyles.boldLabel);
        //             EditorGUILayout.ObjectField(level, typeof(Level), false, GUILayout.MinWidth(200f), GUILayout.MaxWidth(400f));
        //             EditorGUILayout.Separator();
        //             var so = new SerializedObject(level);
        //             var tra = so.FindProperty("areas");
        //             int t = 0;
        //             int anr = 1;
        //             areaBools[t] = EditorGUILayout.Foldout(areaBools[t], "Areas");
        //             if (areaBools[t])
        //                 if (level.areas.Length != 0)
        //                 {
        //                     int i;
        //                     for (i = 0; i < level.areas.Length; i++)
        //                     {
        //                         int wnr = 1;
        //                         var a = tra.GetArrayElementAtIndex(i);
        //                         GUILayout.Label("Area " + anr, EditorStyles.boldLabel);
        //                         waveBools[t] = EditorGUILayout.Foldout(waveBools[t], "Waves");
        //                         if (waveBools[t])
        //                             if (level.areas[i].waves.Length != 0)
        //                             {
        //                                 int j;
        //                                 for (j = 0; j < level.areas[i].waves.Length; j++)
        //                                 {
        //                                     int spnr = 1;
        //                                     GUILayout.Label("Wave " + wnr, EditorStyles.boldLabel);
        //                                     var w = a.FindPropertyRelative("waves").GetArrayElementAtIndex(j);
        //                                     var s = w.FindPropertyRelative("SpawnNextWaveInstantly").boolValue = EditorGUILayout.Toggle(w.FindPropertyRelative("SpawnNextWaveInstantly").boolValue);
        //                                     spawnBools[t] = EditorGUILayout.Foldout(spawnBools[t], "SpawnPoints");
        //                                     if (spawnBools[t])
        //                                         if (level.areas[i].waves[j].spawnPoints.Length != 0)
        //                                         {
        //                                             int x;
        //                                             for (x = 0; x < level.areas[i].waves[j].spawnPoints.Length; x++)
        //                                             {
        //                                                 GUILayout.Label("Spawn " + spnr, EditorStyles.boldLabel);
        //                                                 var sp = w.FindPropertyRelative("spawnPoints").GetArrayElementAtIndex(x);
        //                                                 var pre = sp.FindPropertyRelative("enemyType").enumValueIndex = EditorGUILayout.EnumPopup("prefab", level.areas[i].waves[j].spawnPoints[x].enemyType, GUILayout.MinWidth(200f), GUILayout.MaxWidth(maxWidth)).GetHashCode();
        //                                                 var spawnp = sp.FindPropertyRelative("UniqueID").intValue = EditorGUILayout.IntField("Point", level.areas[i].waves[j].spawnPoints[x].UniqueID, GUILayout.MinWidth(200f), GUILayout.MaxWidth(maxWidth));
        //                                                 spnr++;
        //                                                 so.ApplyModifiedProperties();

        //                                             }
        //                                             if (GUILayout.Button("Add Spawnpoint"))
        //                                             {
        //                                                 w.FindPropertyRelative("spawnPoints").InsertArrayElementAtIndex(level.areas[j].waves[x].spawnPoints.Length);
        //                                                 so.ApplyModifiedProperties();
        //                                                 spawnBools.Add(false);

        //                                             }
        //                                             if (level.areas[j].waves[x].spawnPoints.Length != 0)
        //                                                 if (GUILayout.Button("Remove Spawnpoint"))
        //                                                 {
        //                                                     w.FindPropertyRelative("spawnPoints").GetArrayElementAtIndex(level.areas[j].waves[x].spawnPoints.Length - 1).objectReferenceValue = null;
        //                                                     w.FindPropertyRelative("spawnPoints").DeleteArrayElementAtIndex(level.areas[j].waves[x].spawnPoints.Length - 1);
        //                                                     so.ApplyModifiedProperties();
        //                                                     spawnBools.RemoveAt(spawnBools.Count);
        //                                                 }
        //                                         }


        //                                     wnr++;
        //                                     so.ApplyModifiedProperties();

        //                                 }
        //                                 if (GUILayout.Button("Add Wave"))
        //                                 {
        //                                     a.FindPropertyRelative("waves").InsertArrayElementAtIndex(level.areas[j].waves.Length);
        //                                     so.ApplyModifiedProperties();
        //                                     waveBools.Add(false);
        //                                 }
        //                                 if (level.areas[j].waves.Length != 0)
        //                                     if (GUILayout.Button("Remove Wave"))
        //                                     {
        //                                         a.FindPropertyRelative("waves").GetArrayElementAtIndex(level.areas[j].waves.Length - 1).objectReferenceValue = null;
        //                                         a.FindPropertyRelative("waves").DeleteArrayElementAtIndex(level.areas[j].waves.Length - 1);
        //                                         so.ApplyModifiedProperties();
        //                                         waveBools.RemoveAt(waveBools.Count);
        //                                     }
        //                             }



        //                         so.ApplyModifiedProperties();
        //                         EditorGUILayout.Separator();

        //                         anr++;

        //                     }
        //                     if (GUILayout.Button("Add Area"))
        //                     {
        //                         so.FindProperty("areas").InsertArrayElementAtIndex(level.areas.Length);
        //                         so.ApplyModifiedProperties();
        //                         areaBools.Add(false);

        //                     }
        //                     if (level.areas.Length != 0)
        //                         if (GUILayout.Button("Remove Area"))
        //                         {
        //                             so.FindProperty("areas").GetArrayElementAtIndex(level.areas.Length - 1).objectReferenceValue = null;
        //                             so.FindProperty("areas").DeleteArrayElementAtIndex(level.areas.Length - 1);
        //                             so.ApplyModifiedProperties();
        //                             areaBools.RemoveAt(areaBools.Count);
        //                         }
        //                 }


        //             so.ApplyModifiedProperties();
        //             t++;

        //         }
    }
}
