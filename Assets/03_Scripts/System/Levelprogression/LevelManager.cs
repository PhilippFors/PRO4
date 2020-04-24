using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int currentLevel = 0;
    [SerializeField] private int currentWave = 0;
    [SerializeField] private int currentArea = 0;
    public InSceneSO level0;
    [SerializeField] private Level[] levels;

    private void Start()
    {
        LevelEventSystem.instance.areaEntry += StartArea;
        LevelEventSystem.instance.nextWave += StartNextWave;
        LevelEventSystem.instance.areaExit += AreaFinsihed;
        levels[0] = level0.levelInfo;
        // LevelEventSystem.instance.levelExit += LevelFinished;
    }

    private void OnDisable()
    {
        LevelEventSystem.instance.areaEntry -= StartArea;
        LevelEventSystem.instance.nextWave -= StartNextWave;
        LevelEventSystem.instance.areaExit -= AreaFinsihed;
    }

    bool HasNextWave()
    {
        return currentWave + 1 <= levels[currentLevel].areas[currentArea].waves.Length;
    }

    void AreaFinsihed()
    {
        levels[currentLevel].areas[currentArea].finished = true;
        currentArea++;
        currentWave = 1;
        if (currentArea + 1 == levels[currentLevel].areas.Length)
        {
            LevelFinished();
        }
    }

    void LevelFinished()
    {
        currentLevel++;
        currentArea = 1;
        currentWave = 1;
    }

    void StartArea()
    {
        if (!levels[currentLevel].areas[currentArea].started)
        {
            Spawn();
            currentWave++;
            levels[currentLevel].areas[currentArea].started = true;
        }
    }

    void StartNextWave()
    {
        if (HasNextWave())
        {
            Spawn();
            currentWave++;
        }
    }

    public void Spawn()
    {
        SpawnManager.instance.SpawnEnemies(levels[currentLevel].areas[currentArea].waves[currentWave], currentWave);
    }

}
