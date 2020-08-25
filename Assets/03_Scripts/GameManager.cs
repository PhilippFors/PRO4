using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //GameManager for starting games and managing game over states
    public PlayerBody player;
    public void StartGame()
    {
        if (SaveManager.instance.isNewGame)
        {
            player.InitStats(player.template);
            SaveManager.instance.LoadLevel();
        }
        else
        {
            SaveManager.instance.LoadPlayer();
        }
    }

    public void GameOver()
    {

    }
}
