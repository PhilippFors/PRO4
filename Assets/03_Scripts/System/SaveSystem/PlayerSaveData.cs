using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    public float currentPlayerHealth;
    public float playerLvl;
    public float[] playerPos;
    public Dictionary<string, float> playerStats;
    public Dictionary<string, int> skillLevels;
    public int currentWeaponID;
    public List<int> equipedWeaponIDs;
    public List<int> unlockedWeapons;
    public List<int> unlockedSkills;


    public PlayerSaveData(PlayerBody playerBody, PlayerAttack playerAttack)
    {
        SavePlayerStats(playerBody);
        SaveWeaponData(playerAttack);
    }

    void SavePlayerStats(PlayerBody playerBody)
    {
        currentPlayerHealth = playerBody.currentHealth.Value;
        playerPos = new float[3];
        playerPos[0] = playerBody.transform.position.x;
        playerPos[1] = playerBody.transform.position.y;
        playerPos[2] = playerBody.transform.position.z;

        playerStats = new Dictionary<string, float>();

        foreach (GameStatistics stat in playerBody.statList)
        {
            playerStats.Add(stat.GetName().ToString(), stat.GetValue());
        }
    }

    void SaveWeaponData(PlayerAttack playerAttack)
    {
        equipedWeaponIDs = new List<int>();

        currentWeaponID = playerAttack.currentWeapon.stats.weaponID;

        foreach (Weapons w in playerAttack.weapons)
            equipedWeaponIDs.Add(w.stats.weaponID);
    }
}
