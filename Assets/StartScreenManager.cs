using System.Collections;
using UnityEngine;
public class StartScreenManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Enter());
    }

    IEnumerator Enter()
    {
        GameManager.instance.transitionCanvas.gameObject.SetActive(true);
        GameManager.instance.transitionImage.Play("InTransition");
        yield return new WaitForSeconds(GameManager.instance.transitionImage.GetClip("InTransition").length);
        GameManager.instance.transitionCanvas.gameObject.SetActive(false);
    }
    public void NewGame()
    {
        GameManager.instance.StartGame();
    }

    public void Arena()
    {
        GameManager.instance.StartArena();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
