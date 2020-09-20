using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SpawnPointWorker : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    public int LevelID;
    public int AreaID;
    public int UniqueID;
    int oldLevelID;

    public bool playerSpawnpoint = false;
    List<SpawnpointData> queue = new List<SpawnpointData>();
    bool isSpawning = false;

    void Start()
    {
        oldLevelID = LevelID;
    }

    public void UpdateID()
    {
        SpawnPointWorker[] list = FindObjectsOfType<SpawnPointWorker>();
        if (LevelID != oldLevelID)
        {
            foreach (SpawnPointWorker id in list)
            {
                id.LevelID = this.LevelID;
            }
            oldLevelID = LevelID;
        }
        foreach (SpawnPointWorker id in list)
        {
            if (id.playerSpawnpoint)
                id.gameObject.name = "PlayerSpawnPoint";
            else
                id.gameObject.name = "SpawnPNT: " + "Lvl " + id.LevelID + ", ar " + id.AreaID + ", Unq " + id.UniqueID;
        }
    }

    public void AddToQueue(SpawnpointData spawnPoint, bool scriptedSpawn, SpawnManager sp)
    {
        queue.Add(spawnPoint);

        if (!isSpawning)
            StartCoroutine(WorkQueue(scriptedSpawn, sp));
    }

    IEnumerator WorkQueue(bool scriptedSpawn, SpawnManager sp)
    {
        isSpawning = true;
        EnemyBody enemy = null;

        for (int i = 0; i < queue.Count; i++)
        {
            director.Play();
            yield return new WaitForSeconds(sp.spawnAnimDelay);
            switch (queue[i].enemyType)
            {
                case EnemyType.Avik:
                    enemy = InstEnemy(sp.Avik);
                    break;
                case EnemyType.Shentau:
                    enemy = InstEnemy(sp.Shentau);
                    break;
                case EnemyType.undefinded:

                    break;
            }
            SceneManager.MoveGameObjectToScene(enemy.parent, SceneManager.GetSceneByName("Base"));
            enemy.GetComponent<StateMachineController>().aiManager = sp.manager;
            enemy.GetComponent<Animation>().Play("Entry");

            sp.AddEnemyToList(enemy);

            if (scriptedSpawn)
            {
                StartCoroutine(WaitForAnimation(enemy, true));

            }
            else
            {
                StartCoroutine(WaitForAnimation(enemy));
            }


            if (i + 1 < queue.Count)
                yield return new WaitForSeconds(sp.SpawnWaitTime);
        }

        queue.Clear();
        sp.count = true;
        isSpawning = false;
    }

    EnemyBody InstEnemy(GameObject obj)
    {
        return Instantiate(obj, transform.position, transform.localRotation).gameObject.GetComponentInChildren<EnemyBody>();
    }

    IEnumerator WaitForAnimation(EnemyBody enemy, bool scripted = false)
    {
        while (enemy.gameObject.GetComponent<Animation>().isPlaying)
        {
            yield return null;
        }
        if (!scripted)
            EventSystem.instance.ActivateAI(enemy);
        else
            StoryEventSystem.instance.ShowPrompt();
    }
}
