using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum BaseScenes
{
    Manager = 0,
    StartMenu = 1,
    Base = 2,

    Arena = 4
}
public class SceneLoader : MonoBehaviour
{
    [Header("Levels")]
    public List<Scene> levelList = new List<Scene>();

    ///<summary>
    ///Loads one of the Main Scenes like Base, Start Menu, UI Scene, etc.
    ///</summary>
    public void LoadMainScenes(BaseScenes scene)
    {

        switch (scene)
        {
            case BaseScenes.Base:
                SceneManager.LoadSceneAsync((int)BaseScenes.Base, LoadSceneMode.Additive);
                break;
            case BaseScenes.StartMenu:
                SceneManager.LoadSceneAsync((int)BaseScenes.StartMenu, LoadSceneMode.Additive);
                break;
        }
    }
    private void Start()
    {

    }
    public void LoadFirstLevel()
    {
        StartCoroutine(LevelTransition(levelList[0].buildIndex));
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LevelTransition(levelList[GameManager.instance.currentLevel + 1].buildIndex, levelList[GameManager.instance.currentLevel].buildIndex));
    }

    public void LoadPreviousLevel()
    {
        StartCoroutine(LevelTransition(levelList[GameManager.instance.currentLevel - 1].buildIndex, levelList[GameManager.instance.currentLevel].buildIndex));
    }
    public void UnloadScene(BaseScenes scene)
    {
        SceneManager.UnloadSceneAsync(scene.ToString());
    }
    public void LoadSpecificLevel(int i)
    {
        StartCoroutine(LevelTransition(levelList[i].buildIndex, levelList[GameManager.instance.currentLevel].buildIndex));
    }

    public void LoadScene(int newScene, int oldScene, int oldScene2 = -1)
    {
        StartCoroutine(LevelTransition(newScene, oldScene, oldScene2));
    }
    public IEnumerator LevelTransition(int newScene, int oldScene = -1, int oldScene2 = -1)
    {
        //Level End Fade to Black
        AsyncOperation unload;
        AsyncOperation load;
        yield return new WaitForSeconds(1f);
        if (oldScene != -1)
        {
            unload = SceneManager.UnloadSceneAsync(oldScene);
            yield return unload;
        }

        if (oldScene2 != -1)
        {
            unload = SceneManager.UnloadSceneAsync(oldScene2);
            yield return unload;
        }
        if (!SceneManager.GetSceneByName("Base").isLoaded)
        {
            yield return new WaitForEndOfFrame();
            load = SceneManager.LoadSceneAsync("Base", LoadSceneMode.Additive);
            yield return load;
        }
        load = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);

        yield return load;


        yield return new WaitForSeconds(1f);
        //Level start fade from black
    }

}
