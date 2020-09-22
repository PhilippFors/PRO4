using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    //GameManager for starting games and managing game over states
    public Canvas transitionCanvas;
    public Animation transitionImage;
    public event System.Action initAll;
    public event System.Action deInitAll;
    public int currentLevel = 0;
    public bool arena = false;
    public bool isNewGame = true;
    public bool editing = true;
    public bool gamePaused = false;
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

    public void InitAll()
    {
        if (initAll != null)
            initAll();
    }

    public void DeInitAll()
    {
        if (deInitAll != null)
            deInitAll();
    }
    public void StartArena()
    {
        arena = true;
        sceneLoader.LoadScene((int)BaseScenes.Arena, true, (int)BaseScenes.StartMenu);
    }
    public void StartGame()
    {
        sceneLoader.LoadScene(sceneLoader.levelList[GameManager.instance.currentLevel].handle, true, (int)BaseScenes.StartMenu);
    }

    public void GameOver()
    {


    }

    public void Respawn(PlayerBody player, Vector3 respawn)
    {
        StartCoroutine(RespawnAnim(player, respawn));
    }

    public void ReturnToStartMenu()
    {
        if (arena)
        {
            sceneLoader.LoadScene((int)BaseScenes.StartMenu, false, (int)BaseScenes.Base, (int)BaseScenes.Arena);
            arena = false;
        }
        else
        {
            sceneLoader.LoadScene((int)BaseScenes.StartMenu, false, sceneLoader.levelList[GameManager.instance.currentLevel].handle, (int)BaseScenes.Base);
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

    IEnumerator RespawnAnim(PlayerBody player, Vector3 respawn)
    {
        transitionCanvas.gameObject.SetActive(true);
        float length = transitionImage.GetClip("InTransition").length;
        transitionImage.Play("InTransition");
        yield return new WaitForSeconds(length);
        player.GetComponent<Animator>().applyRootMotion = false;
        player.transform.position = respawn;
        player.currentHealth.Value = player.GetStatValue(StatName.MaxHealth);
        transitionImage.Play("OutTransition");
        player.GetComponent<Animator>().applyRootMotion = false;
        player.alive = true;
        yield return new WaitForSeconds(length);
        transitionCanvas.gameObject.SetActive(false);
    }
}
