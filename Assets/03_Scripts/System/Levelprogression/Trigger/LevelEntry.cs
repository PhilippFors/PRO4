using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEntry : MonoBehaviour
{
    public bool sceneLoaded = false;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += StartLevel;
    }

    private void Start()
    {
        StartCoroutine(WaitForSTart());
    }

    IEnumerator WaitForSTart()
    {
        yield return new WaitForEndOfFrame();
        while (!SceneManager.GetSceneByName("Base").isLoaded)
            if (!sceneLoaded)
            {
                Debug.Log("start edit" + Time.time);
                sceneLoaded = true;
                LevelEventSystem.instance.LevelEntry();
            }
    }

    void StartLevel(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Base")
        {
            StartCoroutine(WaitFStart(scene));
        }
    }

    IEnumerator WaitFStart(Scene scene)
    {
        yield return new WaitForEndOfFrame();
        sceneLoaded = true;
        LevelEventSystem.instance.LevelEntry();
        Debug.Log(scene.name + ": " + Time.time);
    }
}
