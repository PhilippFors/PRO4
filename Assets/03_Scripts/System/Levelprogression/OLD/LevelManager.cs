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
    public Level[] levelData;
    public List<SpawnpointID> spawnpointList;
    private void Start()
    {
        LevelEventSystem.instance.areaEntry += StartArea;
        LevelEventSystem.instance.nextWave += StartWave;
        LevelEventSystem.instance.areaExit += AreaFinsihed;
        LevelEventSystem.instance.getList += GetList;
        LevelEventSystem.instance.levelExit += LevelFinished;
    }

    private void OnDisable()
    {
        LevelEventSystem.instance.areaEntry -= StartArea;
        LevelEventSystem.instance.nextWave -= StartWave;
        LevelEventSystem.instance.areaExit -= AreaFinsihed;
        LevelEventSystem.instance.getList -= GetList;
    }

    bool HasNextWave()
    {
        return currentWave + 1 <= levelData[currentLevel].areas[currentArea].waves.Length;
    }

    public void GetList(List<SpawnpointID> list)
    {
        spawnpointList = list;
    }
    void AreaFinsihed()
    {
        levelData[currentLevel].areas[currentArea].finished = true;
        SpawnManager.instance.areaStarted = false;
        currentArea++;
        currentWave = 0;
        StartArea();
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
        if (!levelData[currentLevel].areas[currentArea].started)
        {
            StartWave();
            levelData[currentLevel].areas[currentArea].started = true;
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
        if (!levelData[currentLevel].areas[currentArea].waves[currentWave].SpawnNextWaveInstantly)
        {
            wavesToSpawn.Add(levelData[currentLevel].areas[currentArea].waves[currentWave]);
            currentWave++;
            Spawn(wavesToSpawn);
            return;
        }
        while (true)
        {
            if (i >= levelData[currentLevel].areas[currentArea].waves.Length || !levelData[currentLevel].areas[currentArea].waves[i].SpawnNextWaveInstantly)
            {
                Spawn(wavesToSpawn);
                Debug.Log(wavesToSpawn.Count);
                return;
            }
            else
            {
                wavesToSpawn.Add(levelData[currentLevel].areas[currentArea].waves[i]);
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
        levelData[currentLevel].areas[currentArea].started = false;
        StartArea();
    }


    public void SetCurrentLevel(int level){
        currentLevel = level;
    }

    public void SetCurrentArea(int area){
        currentArea = area;
    }

    public int GetCurrentLevel(){
        return currentLevel;
    }

    public int GetCurrentArea(){
        return currentArea;
    }
}
