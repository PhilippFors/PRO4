using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class SpawnProcess : MonoBehaviour
{
    public PlayableDirector timeline;
    public GameObject parent;
    private Wave wave;

    public void StartSpawnAnim(Wave w)
    {
        wave = w;
        timeline.Play();

        //Do SpawnAnimation

        //When SpawnAnimation Finsished, Start AI        
    }

    public void Spawn()
    {
        for (int i = 0; i < wave.spawnPoints.Length; i++)
        {
            EnemyBody enemy =Instantiate(wave.spawnPoints[i].prefab, wave.spawnPoints[i].point.position, wave.spawnPoints[i].point.localRotation).gameObject.GetComponentInChildren<EnemyBody>();
            enemy.GetComponent<Animation>().Play("Entry");
            SpawnManager.instance.AddEnemyToList(enemy);
        }

    }

}
