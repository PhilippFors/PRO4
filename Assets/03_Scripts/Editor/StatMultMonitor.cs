using UnityEngine;
using UnityEditor;

public class StatMultMonitor : EditorWindow
{
    bool enemyFoldout = false;
    bool playerFoldout = false;

    public EnemySet set;
    public PlayerBody player;

    Vector2 scrollPosition = Vector2.zero;
    Vector2 enemyScrollPosition = Vector2.zero;

    [MenuItem("Tools/Custom Window/Stat-Mult Runtime Monitor")]
    public static void ShowWindow()
    {
        GetWindow<StatMultMonitor>("Stat-Mult Monitor");
    }

    void OnGUI()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBody>();
        set = (EnemySet)Resources.Load("New Enemy Set");
        // GUILayout.Label("Player Stats");

        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUILayout.MinHeight(100f), GUILayout.MaxHeight(130f));
        playerFoldout = EditorGUILayout.Foldout(playerFoldout, "Player");

        if (playerFoldout)
        {
            if (player.statList != null)
            {
                player.currentHealth.Value = EditorGUILayout.FloatField("Current Health", player.currentHealth.Value, GUILayout.MinWidth(150f), GUILayout.MaxWidth(250f));
                foreach (GameStatistics s in player.statList)
                {
                    s.SetValue(EditorGUILayout.FloatField(s.GetName().ToString(), s.GetValue(), GUILayout.MinWidth(150f), GUILayout.MaxWidth(250f)));
                }
            }
        }

        GUILayout.EndScrollView();

        enemyScrollPosition = GUILayout.BeginScrollView(enemyScrollPosition, false, true);
        enemyFoldout = EditorGUILayout.Foldout(enemyFoldout, "Enemies");

        if (enemyFoldout)
            foreach (EnemyBody e in set.entityList)
            {
                if (e != null)
                {
                    GUILayout.Space(5f);
                    GUILayout.Label(e.gameObject.name, EditorStyles.largeLabel);
                    EditorGUILayout.ObjectField(e, typeof(EnemyBody));

                    GUILayout.Label("Stats", EditorStyles.boldLabel);
                    foreach (GameStatistics s in e.statList)
                    {
                        s.SetValue(EditorGUILayout.FloatField(s.GetName().ToString(), s.GetValue(), GUILayout.MinWidth(150f), GUILayout.MaxWidth(250f)));
                    }

                    GUILayout.Label("Multipliers", EditorStyles.boldLabel);
                    foreach (Multiplier m in e.multList)
                    {
                        m.SetValue(EditorGUILayout.FloatField(m.GetName().ToString(), m.GetValue(), GUILayout.MinWidth(150f), GUILayout.MaxWidth(250f)));
                    }
                    if (GUILayout.Button("Destroy", GUILayout.Width(120f)))
                    {
                        set.Remove(e);
                        Destroy(e.parent);
                    }
                }

            }

        GUILayout.EndScrollView();
    }
}
