using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    int level = 1;
    public List<EnemyBaseClass> enemies = new List<EnemyBaseClass>();
    public DestructableObstacleBase[] destructableObstacles;
    public LevelSpawnpoints[] levelArray;
    public void AddEnemy(EnemyBaseClass enemy)
    {
        enemies.Add(enemy);
    }

    public static SpawnManager instance;
    private void Awake()
    {
        instance = this;
    }
    
    public void SpawnEnemies()
    {
        if (levelArray == null)
        {
            Debug.LogError("There are no spawnpoints");
            return;
        }

        for (int i = 0; i < levelArray.Length; i++)
        {
            if (levelArray[i].lvlID == level)
            {
                if (levelArray[i].SpawnPoints != null)
                {
                    foreach (SpawnPoint info in levelArray[i].SpawnPoints)
                    {
                        Instantiate(info.prefab, info.Spawnpoint.position, Quaternion.Euler(info.Spawnpoint.forward));
                        AddEnemy(info.enemy);
                    }
                }
                return;
            }
        }
        //will be done in level progression manager later
        level++;
    }
}
