using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProcess : MonoBehaviour
{
    public void StartSpawnAnim(Wave wave, int waveIndex)
    {

        Spawn(wave);


        //Do SpawnAnimation

        //When SpawnAnimation Finsished, Start AI        
    }

    public void Spawn(Wave wave)
    {
        for (int i = 0; i < wave.spawnPoints.Length; i++)
            Instantiate(wave.spawnPoints[i].prefab, wave.spawnPoints[i].point.position, Quaternion.Euler(wave.spawnPoints[i].point.forward));
    }
}
