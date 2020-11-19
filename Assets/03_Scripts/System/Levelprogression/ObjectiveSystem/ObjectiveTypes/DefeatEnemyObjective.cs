using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author(mainAuthor = "Philipp Forstner")]
[CreateAssetMenu(fileName = "New DefeatEnemyObjective", menuName = "Objectives/DefeatEnemyObjective")]
public class DefeatEnemyObjective : Objective
{
    public WaveData[] waves;
    public int currentWave = 0;
    public bool lastWaveDefeated = false;

    public override void ExecuteObjective(LevelManager manager)
    {
        if (SpawnManager.instance.enemyListEmpty)
        {
            NewWave(manager);
            SpawnManager.instance.StartEnemyCount();
        }
    }

    public void NewWave(LevelManager manager)
    {
        if (currentWave != 0 & !HasNextWave())
        {
            lastWaveDefeated = true;
            return;
        }

        List<WaveData> wavesToSpawn = new List<WaveData>();

        int i = currentWave;
        if (!waves[currentWave].SpawnNextWaveInstantly)
        {
            wavesToSpawn.Add(waves[currentWave]);
            currentWave++;
            SpawnManager.instance.StartSpawn(wavesToSpawn, i);
            return;
        }
        while (true)
        {
            if (i >= waves.Length || !waves[i].SpawnNextWaveInstantly)
            {
                SpawnManager.instance.StartSpawn(wavesToSpawn, i);
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
        base.ObjEnter(manager);
        currentWave = 0;
        NewWave(manager);
        SpawnManager.instance.StartEnemyCount();
    }

    public override void CheckGoal(LevelManager manager)
    {
        if (lastWaveDefeated & letAreaFinish)
        {
            manager.SwitchObjective();
        }
    }

    private void OnDisable()
    {
        this.started = false;
        this.finished = false;
        this.lastWaveDefeated = false;
        currentWave = 0;
    }
}
