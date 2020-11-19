using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GetAuthorAttributes : EditorWindow
{
    int pathlength;
    int oldPathlength;
    string[] arr = { "03_Scripts" };
    Vector2 scrollPosition = Vector2.zero;
    bool secure = false;
    StatVariable v;
    MultVariable m;
    string[] paths;
    string[] stuff; bool found = false;
    bool init = false;
    List<MonoBehaviour> list = new List<MonoBehaviour>();

    List<MonoBehaviour> attributelist = new List<MonoBehaviour>();
    [MenuItem("Tools/Custom Window/Get Author Attribute")]
    public static void ShowWindow()
    {
        GetWindow<GetAuthorAttributes>("Find Authored Scripts");
    }

    void OnGUI()
    {
        if (GUILayout.Button("RELOAD"))
        {
            list.Clear();
            attributelist.Clear();
            init = false;
        }
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true);
        paths = AssetDatabase.FindAssets("t:Object", arr);
        pathlength = paths.Length;

        if (pathlength != oldPathlength)
        {
            list.Clear();
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
            {
                if (AssetDatabase.LoadAssetAtPath(stuff[i], typeof(MonoBehaviour)) != null)
                    list.Add((MonoBehaviour)AssetDatabase.LoadAssetAtPath(stuff[i], typeof(MonoBehaviour)));
            }
            init = true;

            MonoBehaviour[] sceneActive = Resources.FindObjectsOfTypeAll(typeof(MonoBehaviour)) as MonoBehaviour[];

            foreach (MonoBehaviour mono in sceneActive)
            {
                System.Reflection.MemberInfo info = mono.GetType();

                var objectFields = mono.GetType().GetCustomAttributes(typeof(AuthorAttribute), true);
                for (int i = 0; i < objectFields.Length; i++)
                {
                    var attr = typeof(AuthorAttribute);
                    if (objectFields[i] != null)
                    {
                        if (attributelist.Find(x => x.GetType().Equals(mono.GetType())) == null)
                        {
                            Debug.Log(info.ToString());
                            attributelist.Add(mono);
                        }
                        // The name of the flagged variable.
                    }
                }
            }

        }

        foreach (MonoBehaviour mono in attributelist)
            EditorGUILayout.ObjectField(mono.name, mono, typeof(MonoBehaviour), true);


        GUILayout.EndScrollView();
    }

    private void OnInspectorUpdate()
    {
        Repaint();
    }
}
