using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum BaseScenes
{
    undefined,
    Base,
    StartMenu,
    credits,
    UIScene,
    settings
}
public class SceneLoader : MonoBehaviour
{
    public LevelManager levelManager;

    [Header("Main Scenes")]
    public Scene baseScene;
    public Scene StartMenu;
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
            case BaseScenes.Base:
                SceneManager.LoadSceneAsync(BaseScenes.Base.ToString(), LoadSceneMode.Additive);
                break;
            case BaseScenes.StartMenu:
                SceneManager.LoadSceneAsync(StartMenu.name, LoadSceneMode.Single);
                break;
            case BaseScenes.credits:
                SceneManager.LoadSceneAsync(credits.name, LoadSceneMode.Single);
                break;
                // case BaseScenes.settings:
                //     SceneManager.LoadSceneAsync(startMenu.name, LoadSceneMode.Single);
                //     break;
        }
    }
    private void Start()
    {

    }
    public void LoadFirstLevel()
    {
        StartCoroutine(LevelTransition(levelList[0]));
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LevelTransition(levelList[levelManager.currentLevel + 1], levelList[levelManager.currentLevel].name));
    }

    public void LoadPreviousLevel()
    {
        StartCoroutine(LevelTransition(levelList[levelManager.currentLevel - 1], levelList[levelManager.currentLevel].name));
    }
    public void UnloadScene(BaseScenes scene)
    {
        SceneManager.UnloadSceneAsync(scene.ToString());
    }
    public void LoadSpecificLevel(int i)
    {
        StartCoroutine(LevelTransition(levelList[i], levelList[levelManager.currentLevel].name));
    }

    public void LoadScene(string newScene, string oldScene)
    {
        StartCoroutine(LevelTransition(SceneManager.GetSceneByName(newScene), oldScene));
    }
    public IEnumerator LevelTransition(Scene newScene, string oldScene = "")
    {
        //Level End Fade to Black
        yield return new WaitForSeconds(1f);
        AsyncOperation load = SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
        AsyncOperation unload;
        yield return load;

        if (!SceneManager.GetSceneByName("Base").isLoaded)
        {
            load = SceneManager.LoadSceneAsync("Base", LoadSceneMode.Additive);
            yield return load;
        }

        if (oldScene != "")
        {
            unload = SceneManager.UnloadSceneAsync(oldScene);
            yield return unload;
        }


        yield return new WaitForSeconds(1f);
        //Level start fade from black
    }

}
