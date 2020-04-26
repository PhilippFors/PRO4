using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [HideInInspector] public SpawnProcess spawnProcess => GetComponent<SpawnProcess>();
    public float SpawnWaitTime = 4.8f;
    public List<EnemyBody> enemyCollection = new List<EnemyBody>();
    public List<EnemyBody> durga = new List<EnemyBody>();
    public List<EnemyBody> igner = new List<EnemyBody>();

    public bool isSpawning = false;
    public bool areaStarted = false;
    public bool count = false;
    public static SpawnManager instance;
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

    private void Update()
    {
        CountEnemies();
    }

    public void AddEnemyToList(EnemyBody enemy)
    {
        SpawnManager.instance.enemyCollection.Add(enemy);
        EventSystem.instance.AddToStatemachineList(enemy);
        string tag = enemy.gameObject.tag;
        switch (tag)
        {
            case "Durga":
                SpawnManager.instance.durga.Add(enemy);
                break;
            case "Igner":
                SpawnManager.instance.igner.Add(enemy);
                break;
            case "Untagged":
                Debug.Log("No tag found on " + enemy.gameObject.name);
                break;
        }
    }
    void RemoveEnemyFromList(EnemyBody enemy)
    {
        int toRemoveIndex = 0;
        string tag = enemy.gameObject.tag;
        switch (tag)
        {
            case "Durga":
                toRemoveIndex = durga.FindIndex(x => x.GetComponent<EnemyBody>() == enemy);
                durga.RemoveAt(toRemoveIndex);
                break;
            case "Igner":
                toRemoveIndex = igner.FindIndex(x => x.GetComponent<EnemyBody>() == enemy);
                igner.RemoveAt(toRemoveIndex);
                break;
            case "Untagged":
                Debug.Log("No tag found on " + enemy.gameObject.name);
                break;
        }
        toRemoveIndex = enemyCollection.FindIndex(x => x.GetComponent<EnemyBody>() == enemy);
        enemyCollection.RemoveAt(toRemoveIndex);

    }

    void CountEnemies()
    {
        if (enemyCollection.Count == 0 & count)
        {
            if (areaStarted)
            {
                if (!isSpawning)
                {
                    LevelEventSystem.instance.NextWave();
                    count = false;
                }
            }
        }
    }

    public void SpawnEnemies(List<Wave> waves, int waveIndex)
    {
        if (waveIndex == 0)
            StartCoroutine(SpawnDelay(waves));
        else
            StartCoroutine(SpawnDelay(waves, 1.5f));
    }

    IEnumerator SpawnDelay(List<Wave> waves, float wait = 0.5f)
    {
        isSpawning = true;
        yield return new WaitForSeconds(wait);

        if (waves.Count > 1)
        {
            int i = 0;
            while (i < waves.Count)
            {
                Debug.Log(i + " ," + waves.Count);
                spawnProcess.StartSpawnAnim(waves[i]);
                Debug.Log("Spawn");
                yield return new WaitForSeconds(SpawnWaitTime);
                Debug.Log("Wait is over");
                i++;
            }
            isSpawning = false;
            yield break;
        }
        else
        {
            spawnProcess.StartSpawnAnim(waves[0]);
            isSpawning = false;
            yield break;
        }
    }

}
