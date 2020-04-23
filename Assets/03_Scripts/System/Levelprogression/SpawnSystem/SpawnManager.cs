using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [HideInInspector] public SpawnProcess spawnProcess => GetComponent<SpawnProcess>();
    public List<EnemyBody> enemyCollection = new List<EnemyBody>();
    public List<EnemyBody> durga = new List<EnemyBody>();
    public List<EnemyBody> igner = new List<EnemyBody>();

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

    void RemoveEnemyFromList(EnemyBody enemy)
    {
        int toRemoveIndex;
        string tag = enemy.gameObject.tag;
        switch (tag)
        {
            case "Durga":
                toRemoveIndex = durga.FindIndex(x => x.gameObject.name.Equals(enemy.gameObject.name));
                durga.RemoveAt(toRemoveIndex);
                break;
            case "Igner":
                toRemoveIndex = igner.FindIndex(x => x.gameObject.name.Equals(enemy.gameObject.name));
                igner.RemoveAt(toRemoveIndex);
                break;
            case "Untagged":
                Debug.Log("No tag found on " + enemy.gameObject.name);
                break;
        }
        toRemoveIndex = enemyCollection.FindIndex(x => x.gameObject.name.Equals(enemy.gameObject.name));
        enemyCollection.RemoveAt(toRemoveIndex);

        CountEnemies();
    }

    void CountEnemies()
    {
        if (enemyCollection.Count == 0)
        {
            LevelEventSystem.instance.NextWave();
        }
    }
    
    void AddEnemyToList(EnemyBody enemy)
    {
        enemyCollection.Add(enemy);
        string tag = enemy.gameObject.tag;
        switch (tag)
        {
            case "Durga":
                durga.Add(enemy);
                break;
            case "Igner":
                igner.Add(enemy);
                break;
            case "Untagged":
                Debug.Log("No tag found on " + enemy.gameObject.name);
                break;
        }
    }

    public void SpawnEnemies(Wave wave, int waveIndex)
    {
        for (int i = 0; i < wave.spawnPoints.Length; i++)
            AddEnemyToList(wave.spawnPoints[i].enemy);

        if (waveIndex == 0)
            StartCoroutine(SpawnDelay(wave, waveIndex));
        else
            StartCoroutine(SpawnDelay(wave, waveIndex, 2f));
    }

    IEnumerator SpawnDelay(Wave wave, int waveIndex, float wait = 0)
    {
        yield return new WaitForSeconds(wait);
        spawnProcess.StartSpawnAnim(wave, waveIndex);
    }

}
