using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    [SerializeField] private AIManager manager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject Avik;

    public EnemySet enemyCollection;
    public bool isSpawning = false;
    public bool count = false;
    public bool enemyListEmpty = false;

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
                    // LevelEventSystem.instance.NextWave();
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
        isSpawning = true;
        yield return new WaitForSeconds(wait);

        if (waves.Count > 1)
        {
            int i = 0;
            while (i < waves.Count)
            {
                Debug.Log(i + " ," + waves.Count);
                WaveCheck(waves[i]);
                Debug.Log("Spawn");
                yield return new WaitForSeconds(SpawnWaitTime);
                Debug.Log("Wait is over");
                i++;
            }
            yield break;
        }
        else
        {
            WaveCheck(waves[0]);
            yield break;
        }
    }


    void WaveCheck(Wave w)
    {
        foreach (SpawnPoint sPoint in w.spawnPoints)
        {
            switch (sPoint.enemyType)
            {
                case EnemyType.Avik:
                    StartCoroutine(SpawnEnemy(Avik, sPoint.UniqueID));
                    break;
                case EnemyType.undefinded:

                    break;
            }
        }
    }

    IEnumerator SpawnEnemy(GameObject obj, int ID)
    {
        foreach (SpawnpointID spawnPoint in levelManager.spawnpointlist.list)
        {
            if (spawnPoint.UniqueID == ID & spawnPoint.AreaID == levelManager.currentArea & spawnPoint.LevelID == levelManager.currentLevel)
            {
                spawnPoint.director.Play();

                yield return new WaitForSeconds(spawnAnimDelay);

                EnemyBody enemy = Instantiate(obj, spawnPoint.transform.position, spawnPoint.transform.localRotation).gameObject.GetComponentInChildren<EnemyBody>();
                enemy.GetComponent<StateMachineController>().settings = manager;
                enemy.GetComponent<Animation>().Play("Entry");

                AddEnemyToList(enemy);
                StartCoroutine(WaitForAnimation(enemy));

                count = true;
                isSpawning = false;
            }
        }
    }

    IEnumerator WaitForAnimation(EnemyBody enemy)
    {
        while (enemy.gameObject.GetComponent<Animation>().isPlaying)
        {
            yield return null;
        }
        EventSystem.instance.ActivateAI(enemy);
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
