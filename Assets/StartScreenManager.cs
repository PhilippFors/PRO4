using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScreenManager : MonoBehaviour
{
    public void NewGame()
    {
        GameManager.instance.StartGame();
    }

    public void Arena(){
        GameManager.instance.StartArena();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
