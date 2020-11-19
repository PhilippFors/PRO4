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

[Author(mainAuthor = "Philipp Forstner")]
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

    public void LoadSpecificLevel(int i)
    {
        StartCoroutine(LevelTransition(levelList[i].buildIndex, levelList[GameManager.instance.currentLevel].buildIndex));
    }

    public void LoadScene(int newScene, bool loadWithBase, int oldScene = -1, int oldScene2 = -1)
    {
        if (loadWithBase)
            StartCoroutine(LevelTransition(newScene, oldScene, oldScene2));
        else
            StartCoroutine(LoadSceneTransition(newScene, oldScene, oldScene2));
    }

    public IEnumerator LevelTransition(int newScene, int oldScene = -1, int oldScene2 = -1)
    {
        //Level End Fade to Black
        AsyncOperation unload;
        AsyncOperation load;

        GameManager.instance.transitionCanvas.gameObject.SetActive(true);
        float length = GameManager.instance.transitionImage.GetClip("OutTransition").length;
        GameManager.instance.transitionImage.Play("OutTransition");
        yield return new WaitForSeconds(length);
        GameManager.instance.DeInitAll();
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




        load = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);

        yield return load;
        yield return new WaitForEndOfFrame();
        if (!SceneManager.GetSceneByName("Base").isLoaded)
        {
            load = SceneManager.LoadSceneAsync("Base", LoadSceneMode.Additive);
            yield return load;
        }

        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.InitAll();
        GameManager.instance.transitionImage.Play("InTransition");
        yield return new WaitForSeconds(length);
        // GameManager.instance.transitionCanvas.gameObject.SetActive(false);
        //Level start fade from black
    }

    public IEnumerator LoadSceneTransition(int newScene, int oldScene = -1, int oldScene2 = -1)
    {
        GameManager.instance.transitionCanvas.gameObject.SetActive(true);
        AsyncOperation unload;
        AsyncOperation load;
        float length = GameManager.instance.transitionImage.GetClip("OutTransition").length;
        GameManager.instance.transitionImage.Play("OutTransition");
        yield return new WaitForSeconds(length);
        GameManager.instance.DeInitAll();

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

        load = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);

        yield return load;
        GameManager.instance.transitionCanvas.overrideSorting = true;
        yield return new WaitForEndOfFrame();
        GameManager.instance.InitAll();
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.transitionImage.Play("InTransition");
        yield return new WaitForSeconds(length);
        // GameManager.instance.transitionCanvas.gameObject.SetActive(false);
    }

}
