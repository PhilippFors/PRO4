using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //GameManager for starting games and managing game over states
    public bool isNewGame = true;
    [SerializeField] private PlayerBody player;
    public static GameManager instance;
    public SceneLoader sceneLoader;
    private void Awake() {
        instance = this;
    }

    private void Start() {
        StartGame();
    }
    
    public void StartGame()
    {
        if (isNewGame)
        {
            player.InitStats(player.template);
        }
        else
        {
            SaveManager.instance.LoadPlayer();
        }
    }

    public void GameOver()
    {


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
