using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class SpawnProcess : MonoBehaviour
{
    private Wave wave;
    private static int SpawnIndex;

    private List<SpawnPoint> Spawnpoints = new List<SpawnPoint>();
    public void StartSpawnAnim(Wave w)
    {
        Spawnpoints.Clear();
        SpawnIndex = 0;
        wave = w;

        foreach (SpawnPoint spawnPoint in wave.spawnPoints)
        {
            spawnPoint.point.GetComponent<PlayableDirector>().Play();
            Spawnpoints.Add(spawnPoint);
        }
    }


    public void Spawn()
    {
        EnemyBody enemy = Instantiate(Spawnpoints[SpawnIndex].prefab, Spawnpoints[SpawnIndex].point.position, Spawnpoints[SpawnIndex].point.localRotation).gameObject.GetComponentInChildren<EnemyBody>();
        enemy.GetComponent<Animation>().Play("Entry");
        SpawnManager.instance.AddEnemyToList(enemy);
        SpawnIndex++;
        SpawnManager.instance.count = true;
    }

}
