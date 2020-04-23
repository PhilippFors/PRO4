using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<EnemyBody> enemies = new List<EnemyBody>();
    // public ObstacleBody[] destructableObstacles;
    public static SpawnManager instance;
    private void Awake()
    {
        instance = this;
        
    }
    private void Start()
    {
        EventSystem.instance.onEnemyDeath += RemoveEnemy;
    }

    private void OnDisable()
    {
        EventSystem.instance.onEnemyDeath -= RemoveEnemy;
    }

    private void Update()
    {
        
    }

    void CountEnemies()
    {
        if (enemies.Count == 0)
        {
            LevelEventSystem.instance.NextWave();
        }
    }

    void RemoveEnemy(EnemyBody enemy)
    {
        int toRemoveIndex = enemies.FindIndex(x => x.gameObject.name.Equals(enemy.gameObject.name));
        enemies.RemoveAt(toRemoveIndex);
        CountEnemies();
    }

    void AddEnemy(EnemyBody enemy)
    {
        enemies.Add(enemy);
        EventSystem.instance.AddToAIMAnager(enemy);
    }

    public void SpawnEnemies(Wave wave)
    {
        for(int i = 0; i<wave.spawnPoints.Length; i++)
        {
            Instantiate(wave.spawnPoints[i].prefab, wave.spawnPoints[i].point.position, Quaternion.Euler(wave.spawnPoints[i].point.forward));
            AddEnemy(wave.spawnPoints[i].enemy);
        }
    }
}
