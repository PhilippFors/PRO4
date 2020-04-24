using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [HideInInspector] public SpawnProcess spawnProcess => GetComponent<SpawnProcess>();
    [SerializeField] public List<EnemyBody> enemyCollection = new List<EnemyBody>();
    [SerializeField] public List<EnemyBody> durga = new List<EnemyBody>();
    [SerializeField] public List<EnemyBody> igner = new List<EnemyBody>();

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
    public void AddEnemyToList(EnemyBody enemy)
    {
        SpawnManager.instance.enemyCollection.Add(enemy);
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
    
    public void SpawnEnemies(Wave wave, int waveIndex)
    {

        if (waveIndex == 0)
            StartCoroutine(SpawnDelay(wave));
        else
            StartCoroutine(SpawnDelay(wave, 2f));
    }

    IEnumerator SpawnDelay(Wave wave, float wait = 0)
    {
        yield return new WaitForSeconds(wait);
        spawnProcess.StartSpawnAnim(wave);
    }

}
