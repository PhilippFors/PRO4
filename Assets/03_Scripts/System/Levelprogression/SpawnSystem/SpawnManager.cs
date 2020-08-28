using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    [SerializeField] private AIManager manager;
    [SerializeField] private LevelManager levelManager;

    public SpawnpointlistSO spawnpointlist;
    Wave lastWave;
    public EnemySet enemyCollection;
    public bool isSpawning = false;
    [HideInInspector] public bool count = false;
    public bool enemyListEmpty = false;
    [Header("EnemyPrefabs")]
    public GameObject Avik;

    [Header("Settings")]
    public float SpawnWaitTime = 4.8f;
    public float checkDelay = 0.5f;
    public float spawnAnimDelay = 0.5f;
    public float spawnDelay = 1.5f;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        EventSystem.instance.onEnemyDeath += RemoveEnemyFromList;
    }

    private void OnDisable()
    {
        EventSystem.instance.onEnemyDeath -= RemoveEnemyFromList;
    }

    public void AddEnemyToList(EnemyBody enemy)
    {

        enemyCollection.Add(enemy);

        // string tag = enemy.gameObject.tag;
        // switch (tag)
        // {
        //     case "Durga":
        //         durga.Add(enemy);
        //         break;
        //     case "Igner":
        //         igner.Add(enemy);
        //         break;
        //     case "Untagged":
        //         Debug.Log("No tag found on " + enemy.gameObject.name);
        //         break;
        // }
    }
    void RemoveEnemyFromList(EnemyBody enemy)
    {
        // string tag = enemy.gameObject.tag;
        // switch (tag)
        // {
        //     case "Durga":
        //         durga.Remove(enemy);
        //         break;
        //     case "Igner":

        //         igner.Remove(enemy);
        //         break;
        //     case "Untagged":
        //         Debug.Log("No tag found on " + enemy.gameObject.name);
        //         break;
        // }

        enemyCollection.Remove(enemy);
    }

    bool CountEnemies()
    {
        if (enemyCollection.entityList.Count == 0 & count)
        {
            if (levelManager.currentObjective.started)
            {
                if (!isSpawning)
                {
                    count = false;
                    return true;
                }
            }
        }
        return false;
    }

    public void StartSpawn(List<Wave> waves, int waveIndex)
    {
        if (waveIndex == 0)
            StartCoroutine(SpawnDelay(waves));
        else
            StartCoroutine(SpawnDelay(waves, spawnDelay));
    }

    IEnumerator SpawnDelay(List<Wave> waves, float wait = 0.5f)
    {
        lastWave = null;
        yield return new WaitForSeconds(wait);

        if (waves.Count > 1)
        {
            int i = 0;
            while (i < waves.Count)
            {
                if (!isSpawning)
                {
                    isSpawning = true;
                    SpawnEnemy(waves[i]);
                    yield return new WaitForSeconds(SpawnWaitTime);
                    i++;
                }
                yield return null;
            }
            yield break;
        }
        else
        {
            SpawnEnemy(waves[0]);
            yield break;
        }
    }


    void SpawnEnemy(Wave w)
    {
        for (int i = 0; i < w.spawnPoints.Length; i++)
        {
            foreach (SpawnpointID spawnPointID in spawnpointlist.list)
            {
                if (spawnPointID.UniqueID == w.spawnPoints[i].UniqueID & spawnPointID.AreaID == levelManager.currentArea & spawnPointID.LevelID == levelManager.currentLevel)
                {
                    spawnPointID.AddToQueue(w.spawnPoints[i], spawnAnimDelay, SpawnWaitTime, manager, this);
                }
            }
        }
        isSpawning = false;
        lastWave = w;
    }

    public void StartEnemyCount()
    {
        StartCoroutine(CountDelay());
    }

    IEnumerator CountDelay()
    {
        enemyListEmpty = false;
        while (!CountEnemies())
        {
            yield return new WaitForSeconds(checkDelay);
        }
        enemyListEmpty = true;
    }
}
