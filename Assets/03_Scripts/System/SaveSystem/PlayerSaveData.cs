using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    public float currentPlayerHealth;
    public float[] playerPos;
    public Dictionary<string, float> playerStats;
    public PlayerSaveData(PlayerBody playerData)
    {
        currentPlayerHealth = playerData.currentHealth.Value;
        playerPos = new float[3];
        playerPos[0] = playerData.transform.position.x;
        playerPos[1] = playerData.transform.position.y;
        playerPos[2] = playerData.transform.position.z;

        playerStats = new Dictionary<string, float>();
        foreach(GameStatistics stat in playerData.statList){
            playerStats.Add(stat.GetName().ToString(), stat.GetValue());
        }
    }
}
