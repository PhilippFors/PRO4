using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class StatMultMonitor : EditorWindow
{
    bool enemyFoldout = false;
    bool playerFoldout = false;
    public List<bool> folds = new List<bool>();
    public EnemySet set;
    public PlayerBody player;
    public bool init = false;

    Vector2 scrollPosition = Vector2.zero;
    Vector2 enemyScrollPosition = Vector2.zero;
    [MenuItem("Tools/Stat-Mult Monitor")]
    public static void ShowWindow()
    {
        GetWindow<StatMultMonitor>("StatMultMonitor");

    }

    void OnGUI()
    {
        float height = 20f;
        if (!playerFoldout)
        {
            height = 20f;
        }
        else
        {
            height = 150f;
        }
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUILayout.Height(height));
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBody>();
        set = (EnemySet)Resources.Load("New Enemy Set");
        // GUILayout.Label("Player Stats");

        playerFoldout = EditorGUILayout.Foldout(playerFoldout, "Player");
        if (playerFoldout)
        {

            EditorGUILayout.TextField("Current Health", player.currentHealth.Value.ToString(), EditorStyles.boldLabel);
            foreach (GameStatistics s in player.statList)
            {
                EditorGUILayout.TextField(s.GetName().ToString(), s.GetValue().ToString(), EditorStyles.boldLabel);
            }

        }

        GUILayout.EndScrollView();

        enemyScrollPosition = GUILayout.BeginScrollView(enemyScrollPosition, false, true);
        enemyFoldout = EditorGUILayout.Foldout(enemyFoldout, "Enemies");
        if (enemyFoldout)
            foreach (EnemyBody e in set.entityList)
            {
                if (e == null)
                    return;
                GUILayout.Space(10f);
                GUILayout.Label(e.gameObject.name, EditorStyles.largeLabel);
                EditorGUILayout.ObjectField(e, typeof(EnemyBody));

                GUILayout.Label("Stats", EditorStyles.boldLabel);
                foreach (GameStatistics s in e.statList)
                {
                    // EditorGUILayout.DoubleField(s.GetName().ToString(), );
                    EditorGUILayout.TextField(s.GetName().ToString(), s.GetValue().ToString(), EditorStyles.boldLabel);
                }
                GUILayout.Label("Multipliers", EditorStyles.boldLabel);

                foreach (Multiplier m in e.multList)
                {
                    // EditorGUILayout.DoubleField(s.GetName().ToString(), );
                    EditorGUILayout.TextField(m.GetName().ToString(), m.GetValue().ToString(), EditorStyles.boldLabel);
                }
            }

        GUILayout.EndScrollView();

    }

}
