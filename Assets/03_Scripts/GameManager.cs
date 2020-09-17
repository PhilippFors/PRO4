using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //GameManager for starting games and managing game over states
    public int currentLevel = 0;
    public bool arena = false;
    public bool isNewGame = true;
    public bool editing = true;

    public static GameManager instance;
    public SceneLoader sceneLoader;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (!editing)
            sceneLoader.LoadMainScenes(BaseScenes.StartMenu);
    }

    public void StartArena()
    {
        arena = true;
        sceneLoader.LoadScene((int)BaseScenes.Arena, (int)BaseScenes.StartMenu);
    }
    public void StartGame()
    {
        sceneLoader.LoadScene(sceneLoader.levelList[GameManager.instance.currentLevel].handle, (int)BaseScenes.StartMenu);
    }

    public void GameOver()
    {


    }

    public void ReturnToStartMenu()
    {
        if (arena)
        {
            sceneLoader.LoadScene((int)BaseScenes.StartMenu, (int)BaseScenes.Base, (int)BaseScenes.Arena);
        }
        else
        {
            sceneLoader.LoadScene((int)BaseScenes.StartMenu, (int)BaseScenes.Base, sceneLoader.levelList[GameManager.instance.currentLevel].handle);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartFromLastSave()
    {
        SaveManager.instance.LoadLevel();
        SaveManager.instance.LoadPlayer();
    }

    public void RestartLevel()
    {

    }
}
