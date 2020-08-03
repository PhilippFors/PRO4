using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DefeatEnemyObjective", menuName = "Objectives/DefeatEnemyObjective")]
public class DefeatEnemyObjective : Objective
{
    public Wave[] waves;
    int currentWave;
    public bool lastWaveDefeated = false;
    
    public override void ExecuteObjective(LevelManager manager)
    {
        if (manager.spawnManager.enemyListEmpty)
        {
            NewWave(manager);
            manager.spawnManager.StartEnemyCount();
        }
    }

    public void NewWave(LevelManager manager)
    {
        if (!HasNextWave())
        {
            lastWaveDefeated = true;
            return;
        }

        List<Wave> wavesToSpawn = new List<Wave>();

        int i = currentWave;
        if (!waves[currentWave].SpawnNextWaveInstantly)
        {
            wavesToSpawn.Add(waves[currentWave]);
            currentWave++;
            manager.spawnManager.StartSpawn(wavesToSpawn, i);
            return;
        }
        while (true)
        {
            if (i >= waves.Length || !waves[i].SpawnNextWaveInstantly)
            {
                manager.spawnManager.StartSpawn(wavesToSpawn, i);
                Debug.Log(wavesToSpawn.Count);
                return;
            }
            else
            {
                wavesToSpawn.Add(waves[i]);
                currentWave++;
            }
            i++;
        }
    }

    bool HasNextWave()
    {
        return currentWave + 1 <= waves.Length;
    }

    public override void ObjEnter(LevelManager manager)
    {
        NewWave(manager);
        manager.spawnManager.StartEnemyCount();
    }

    public override void ObjExit(LevelManager manager)
    {
        Debug.Log("Objective Exit");
    }

    public override void CheckTransitions(LevelManager manager)
    {
        if (lastWaveDefeated)
        {
            manager.SwitchObjective();
        }
    }

    // IEnumerator GeneralDelay(DefeatEnemyObjective obj, float delayTime, LevelManager manager)
    // {
    //     while (!manager.spawnManager.CountEnemies())
    //     {
    //         yield return new WaitForSeconds(delayTime);
    //     }
    //     NewWave(manager);
    //     yield break;
    // }
}
