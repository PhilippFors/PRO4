using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    PlayerControls input;
    [SerializeField] PlayerStateMachine player;

    public GameObject prompt;
    public Text promptText;

    public GameObject pauseMenu;


    private void Start()
    {
        input = player.input;
        input.uiControls.Enable();
        input.uiControls.Pause.performed += ctx => TogglePauseMenu();
    }

    private void OnDisable()
    {
        input.uiControls.Disable();
    }

    public void ReturnToMainMenu()
    {
        GameManager.instance.ReturnToStartMenu();
    }

    #region PauseMenu
    public void TogglePauseMenu()
    {
        if (GameManager.instance.gamePaused)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            input.Gameplay.Enable();
        }
        else
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            input.Gameplay.Disable();
        }

        GameManager.instance.gamePaused = !GameManager.instance.gamePaused;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        GameManager.instance.gamePaused = false;
        pauseMenu.SetActive(false);
        input.Gameplay.Enable();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        GameManager.instance.gamePaused = true;
        pauseMenu.SetActive(true);
        input.Gameplay.Disable();
    }

    #endregion

    public void ShowPrompt(string text)
    {
        promptText.text = text;
        prompt.SetActive(true);
    }

    public void DisablePrompt()
    {
        prompt.SetActive(false);
    }

}
