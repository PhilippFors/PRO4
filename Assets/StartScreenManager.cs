using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    public SceneLoader sceneLoader;
    public void NewGame()
    {
        sceneLoader.LoadScene(sceneLoader.levelList[0].name, "StartMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
