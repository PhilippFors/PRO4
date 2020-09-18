using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEntry : MonoBehaviour
{
    bool sceneLoaded = false;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += StartLevel;
    }

    private void Awake()
    {
        if (!sceneLoaded & SceneManager.GetSceneByName("Base").isLoaded)
        {
            sceneLoaded = true;
            StartCoroutine(WaitFStart());
        }
 
    }

    void StartLevel(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Base")
        {
            sceneLoaded = true;
            StartCoroutine(WaitFStart());
        }
    }

    IEnumerator WaitFStart()
    {
        yield return new WaitForEndOfFrame();
        LevelEventSystem.instance.LevelEntry();
        SceneManager.sceneLoaded -= StartLevel;
    }
}
