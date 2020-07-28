using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class SpawnpointEditor : EditorWindow
{
    bool choosen = true;
    float maxWidth = 400f;
    List<Level> list = new List<Level>();
    string[] path;
    [MenuItem("Tools/Custom Window/SpawnPoint Editor")]
    public static void ShowWindow()
    {
        GetWindow<SpawnpointEditor>("SpawnPoint Editor");
    }

    void OnGUI()
    {
        GUILayout.Label("Choose a Level in your Asset folder and then click the button below", EditorStyles.boldLabel);
        if (GUILayout.Button("Choose Level", GUILayout.Height(30f)))
        {
            choosen = false;
        }
        if (!choosen)
        {
            list = new List<Level>();
            string[] guid = Selection.assetGUIDs;

            path = new string[guid.Length];
            for (int i = 0; i < guid.Length; i++)
            {
                path[i] = AssetDatabase.GUIDToAssetPath(guid[i]);
            }
            for (int i = 0; i < guid.Length; i++)
                list.Add((Level)AssetDatabase.LoadAssetAtPath(path[i], typeof(Level)));

            choosen = true;
        }

        if (list.Count != 0)
            foreach (Level level in list)
            {
                GUILayout.Label("Level", EditorStyles.boldLabel);
                EditorGUILayout.ObjectField(level, typeof(Level), false, GUILayout.MinWidth(200f), GUILayout.MaxWidth(400f));
                EditorGUILayout.Separator();
                var so = new SerializedObject(level);
                var tra = so.FindProperty("areas");

                int anr = 1;

                if (level.areas.Length != 0)

                    for (int i = 0; i < level.areas.Length; i++)
                    {
                        int wnr = 1;
                        var a = tra.GetArrayElementAtIndex(i);
                        GUILayout.Label("Area " + anr, EditorStyles.boldLabel);

                        if (level.areas[i].waves.Length != 0)
                            for (int j = 0; j < level.areas[i].waves.Length; j++)
                            {
                                int spnr = 1;
                                GUILayout.Label("Wave " + wnr, EditorStyles.boldLabel);
                                var w = a.FindPropertyRelative("waves").GetArrayElementAtIndex(j);
                                var s = w.FindPropertyRelative("SpawnNextWaveInstantly").boolValue = EditorGUILayout.Toggle(w.FindPropertyRelative("SpawnNextWaveInstantly").boolValue);
                                if (level.areas[i].waves[j].spawnPoints.Length != 0)
                                    for (int x = 0; x < level.areas[i].waves[j].spawnPoints.Length; x++)
                                    {
                                        GUILayout.Label("Spawn " + spnr, EditorStyles.boldLabel);
                                        var sp = w.FindPropertyRelative("spawnPoints").GetArrayElementAtIndex(x);
                                        var pre = sp.FindPropertyRelative("enemyType").enumValueIndex = EditorGUILayout.EnumPopup("prefab", level.areas[i].waves[j].spawnPoints[x].enemyType, GUILayout.MinWidth(200f), GUILayout.MaxWidth(maxWidth)).GetHashCode();
                                        var spawnp = sp.FindPropertyRelative("UniqueID").intValue = EditorGUILayout.IntField("Point", level.areas[i].waves[j].spawnPoints[x].UniqueID, GUILayout.MinWidth(200f), GUILayout.MaxWidth(maxWidth));
                                        spnr++;
                                        so.ApplyModifiedProperties();
                                    }
                                wnr++;
                                so.ApplyModifiedProperties();
                            }

                        so.ApplyModifiedProperties();
                        EditorGUILayout.Separator();

                        anr++;

                    }
                so.ApplyModifiedProperties();


            }
    }
}
