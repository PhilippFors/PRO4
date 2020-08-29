using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public LevelManager levelManager;
    public PlayerBody playerBody;
    public PlayerAttack playerAttack;
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
        SaveLoadGame.SaveGameData(playerBody, playerAttack, levelManager);
    }

    public void LoadLevel()
    {
        LevelSaveData data = SaveLoadGame.LoadGameProgress();

        levelManager.SetLevel(data.currentLevel);
        levelManager.SetArea(data.currentArea);
        
        if (data.currentAreaFinsihed)
            for (int j = 0; j <= data.currentArea; j++)
            {
                levelManager.levelData[data.currentLevel].areas[j].finished = true;
                levelManager.levelData[data.currentLevel].areas[j].started = true;
            }

        else
            for (int j = 0; j <= data.currentArea - 1; j++)
            {
                levelManager.levelData[data.currentLevel].areas[j].finished = true;
                levelManager.levelData[data.currentLevel].areas[j].started = true;
            }


    }

    public void LoadPlayer()
    {
        //Load statistics
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

        //Load weapon data
        // foreach(Weapons weapon in playerAttack.weapons){
        //if(weapon.WeaponID == data.currentWeaponID)
        // playerAttack.currentWeapon = weapon;
        // }
        playerAttack.currentWeapon.Equip(playerAttack.weaponPoint);
    }
}
