using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEntry : MonoBehaviour
{
    bool scenLoaded = false;

    private void Start()
    {
        SceneManager.sceneLoaded += StartLevel;

        if (SceneManager.GetSceneByName("Base").isLoaded & !scenLoaded)
        {
            LevelEventSystem.instance.LevelEntry();
        }
    }
    IEnumerator LeStart()
    {
        yield return new WaitForEndOfFrame();
        LevelEventSystem.instance.LevelEntry();
    }

    void StartLevel(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Base")
            LevelEventSystem.instance.LevelEntry();

        scenLoaded = true;
    }

    private void StartL()
    {
        LevelEventSystem.instance.LevelEntry();
        scenLoaded = true;
    }




}
