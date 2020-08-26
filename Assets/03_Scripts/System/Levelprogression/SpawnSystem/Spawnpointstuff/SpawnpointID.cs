using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[ExecuteInEditMode]
public class SpawnpointID : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    public int LevelID;
    public int AreaID;
    public int UniqueID;
    int oldLevelID;

    List<SpawnPoint> queue = new List<SpawnPoint>();
    bool isSpawning = false;
    float animDelay = 0;
    float waitTime = 0;

    void Start()
    {
        oldLevelID = LevelID;
    }

    public void UpdateID()
    {
        SpawnpointID[] list = FindObjectsOfType<SpawnpointID>();
        if (LevelID != oldLevelID)
        {
            foreach (SpawnpointID id in list)
            {
                id.LevelID = this.LevelID;
            }
            oldLevelID = LevelID;
        }
        foreach (SpawnpointID id in list)
        {
            id.gameObject.name = "SpawnPNT: " + "Lvl " + id.LevelID + ", ar " + id.AreaID + ", Unq " + id.UniqueID;
        }
    }

    public void AddToQueue(SpawnPoint spawnPoint, float spawnAnimDelay, float SpawnWaitTime, AIManager manager, SpawnManager spawnM)
    {
        animDelay = spawnAnimDelay;
        waitTime = SpawnWaitTime;

        queue.Add(spawnPoint);

        if (!isSpawning)
            StartCoroutine(WorkQueue(manager, spawnM));
    }

    IEnumerator WorkQueue(AIManager manager, SpawnManager spawnM)
    {
        isSpawning = true;
        EnemyBody enemy = null;
        for (int i = 0; i < queue.Count; i++)
        {
            director.Play();
            yield return new WaitForSeconds(animDelay);
            switch (queue[i].enemyType)
            {
                case EnemyType.Avik:
                    enemy = InstEnemy(spawnM.Avik);
                    break;
                case EnemyType.undefinded:

                    break;
            }
            enemy.GetComponent<StateMachineController>().settings = manager;
            enemy.GetComponent<Animation>().Play("Entry");

            spawnM.AddEnemyToList(enemy);
            StartCoroutine(WaitForAnimation(enemy));
            if (i + 1 < queue.Count)
                yield return new WaitForSeconds(waitTime);
        }
        queue.Clear();
        spawnM.count = true;
        isSpawning = false;
    }

    EnemyBody InstEnemy(GameObject obj)
    {
        return Instantiate(obj, transform.position, transform.localRotation).gameObject.GetComponentInChildren<EnemyBody>();
    }

    IEnumerator WaitForAnimation(EnemyBody enemy)
    {
        while (enemy.gameObject.GetComponent<Animation>().isPlaying)
        {
            yield return null;
        }
        EventSystem.instance.ActivateAI(enemy);
    }
}
