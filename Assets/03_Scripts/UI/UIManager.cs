using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public GameObject prompt;
    public Text promptText;

    public void StartGame()
    {
        SceneManager.LoadScene("Prototype 2");
    }
    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void StartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowPrompt(string text)
    {
        promptText.text = text;
        prompt.SetActive(true);
    }

    public void DisablePrompt(){
        prompt.SetActive(false);
    }
}
