using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    [SerializeField] private int currentLevel = 0;
    [SerializeField] private int currentWave = 0;
    [SerializeField] private int currentArea = 0;
    bool levelExitTriggered = false;
    [SerializeField] private SceneLevelData[] levelData;
    [SerializeField] public Level[] levels;
    private void Start()
    {
        LevelEventSystem.instance.areaEntry += StartArea;
        LevelEventSystem.instance.nextWave += StartWave;
        LevelEventSystem.instance.areaExit += AreaFinsihed;
        levels = new Level[levelData.Length];
        for (int i = 0; i < levelData.Length; i++)
            levels[i] = levelData[i].levelInfo;
        LevelEventSystem.instance.levelExit += LevelFinished;
    }

    private void OnDisable()
    {
        LevelEventSystem.instance.areaEntry -= StartArea;
        LevelEventSystem.instance.nextWave -= StartWave;
        LevelEventSystem.instance.areaExit -= AreaFinsihed;
    }

    bool HasNextWave()
    {
        return currentWave + 1 <= levels[currentLevel].areas[currentArea].waves.Length;
    }

    void AreaFinsihed()
    {
        levels[currentLevel].areas[currentArea].finished = true;
        SpawnManager.instance.areaStarted = false;
        currentArea++;
        currentWave = 0;
        // if (currentArea == levels[currentLevel].areas.Length)
        // {
        //     LevelFinished();
        // }
    }

    void LevelFinished()
    {
        levelExitTriggered = true;
        currentLevel++;
        currentArea = 0;
        currentWave = 0;
    }

    void StartArea()
    {
        levelExitTriggered = false;
        if (!levels[currentLevel].areas[currentArea].started)
        {
            StartWave();
            levels[currentLevel].areas[currentArea].started = true;
        }
    }

    void StartWave()
    {
        if (!HasNextWave())
        {
            AreaFinsihed();
            return;
        }

        List<Wave> wavesToSpawn = new List<Wave>();

        int i = currentWave;
        if (!levels[currentLevel].areas[currentArea].waves[currentWave].SpawnNextWaveInstantly)
        {
            wavesToSpawn.Add(levels[currentLevel].areas[currentArea].waves[currentWave]);
            currentWave++;
            Spawn(wavesToSpawn);
            return;
        }
        while (true)
        {
            if (i >= levels[currentLevel].areas[currentArea].waves.Length || !levels[currentLevel].areas[currentArea].waves[i].SpawnNextWaveInstantly)
            {
                Spawn(wavesToSpawn);
                Debug.Log(wavesToSpawn.Count);
                return;
            }
            else
            {
                wavesToSpawn.Add(levels[currentLevel].areas[currentArea].waves[i]);
                currentWave++;
            }
            i++;
        }
    }

    public void Spawn(List<Wave> wavesToSpawn)
    {
        SpawnManager.instance.SpawnEnemies(wavesToSpawn, currentWave - wavesToSpawn.Count);
    }

    public void RestartLevel()
    {
        
    }
    public void RestartCurrentArea()
    {
        if (currentArea != 0)
            currentArea = currentArea - 1;


        SpawnManager.instance.areaStarted = true;
        levels[currentLevel].areas[currentArea].started = false;
        StartArea();
    }

}
