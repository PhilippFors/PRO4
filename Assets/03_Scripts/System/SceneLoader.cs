using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum BaseScenes
{
    undefined,
    baseScene,
    startMenu,
    credits,
    UIScene,
    settings
}
public class SceneLoader : MonoBehaviour
{
    public LevelManager levelManager;

    [Header("Main Scenes")]
    public Scene baseScene;
    public Scene startMenu;
    public Scene credits;
    public Scene UIScene;
    public Scene settings;

    [Header("Levels")]
    public List<Scene> levelList = new List<Scene>();

    ///<summary>
    ///Loads one of the Main Scenes like Base, Start Menu, UI Scene, etc.
    ///</summary>
    public void LoadMainScenes(BaseScenes scene)
    {

        switch (scene)
        {
            case BaseScenes.baseScene:
                SceneManager.LoadSceneAsync(baseScene.name, LoadSceneMode.Single);
                break;
            case BaseScenes.startMenu:
                SceneManager.LoadSceneAsync(startMenu.name, LoadSceneMode.Single);
                break;
            case BaseScenes.credits:
                SceneManager.LoadSceneAsync(startMenu.name, LoadSceneMode.Single);
                break;
            case BaseScenes.UIScene:
                SceneManager.LoadSceneAsync(startMenu.name, LoadSceneMode.Additive);
                break;
            case BaseScenes.settings:
                SceneManager.LoadSceneAsync(startMenu.name, LoadSceneMode.Single);
                break;
        }
    }

    public void LoadFirstLevel()
    {
        StartCoroutine(LevelTransition(levelList[levelManager.currentLevel].name));
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LevelTransition(levelList[levelManager.currentLevel + 1].name, levelList[levelManager.currentLevel].name));
    }

    public void LoadPreviousLevel()
    {
        StartCoroutine(LevelTransition(levelList[levelManager.currentLevel - 1].name, levelList[levelManager.currentLevel].name));
    }

    public void LoadLoadSpecificLevel(int i)
    {
        StartCoroutine(LevelTransition(levelList[i].name, levelList[levelManager.currentLevel].name));
    }

    public IEnumerator LevelTransition(string newScene, string oldScene = "")
    {
        //Level End Fade to Black
        yield return new WaitForSeconds(1f);
        if (oldScene != "")
            SceneManager.UnloadSceneAsync(oldScene);
        SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);
        yield return new WaitForSeconds(1f);
        //Level start fade from black
    }

}
