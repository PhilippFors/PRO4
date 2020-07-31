using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public LevelManager levelManager;
    public PlayerBody playerBody;
    public static SaveManager instance;
    public bool isNewGame;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (!isNewGame)
        {
            LoadPlayer();
            LoadLevel();
        }
        
    }

    public void Save()
    {
        SaveLoadGame.SaveGameData(playerBody, levelManager);
    }

    public void LoadLevel()
    {
        LevelSaveData data = SaveLoadGame.LoadGameProgress();
        levelManager.SetCurrentLevel(data.currentLevel);
        levelManager.SetCurrentArea(data.currentArea);

        if (data.currentAreaFinsihed)
            for (int i = 0; i < data.currentLevel; i++)
            {
                for (int j = 0; j < data.currentArea; j++)
                {
                    levelManager.levelData[i].areas[j].finished = true;
                }
            }
        else
            for (int i = 0; i <= data.currentLevel; i++)
            {
                for (int j = 0; j <= data.currentArea-1; j++)
                {
                    levelManager.levelData[i].areas[j].finished = true;
                }
            }

    }

    public void LoadPlayer()
    {
        PlayerSaveData data = SaveLoadGame.LoadPlayer();
        playerBody.currentHealth.Value = data.currentPlayerHealth;
        playerBody.transform.position = new Vector3(data.playerPos[0], data.playerPos[1], data.playerPos[2]);
        playerBody.statList = new List<GameStatistics>();
        float o;
        foreach (FloatReference stat in playerBody.template.statList)
        {   
            o = 0;
            StatVariable v = (StatVariable)stat.Variable;
            data.playerStats.TryGetValue(v.statName.ToString(), out o);
            playerBody.statList.Add(new GameStatistics(o, v.statName));
        }
    }
}
