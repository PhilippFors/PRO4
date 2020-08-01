using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private int currentLevel = 0;
    [SerializeField] private int currentWave = 0;
    [SerializeField] private int currentArea = 0;
    bool levelExitTriggered = false;
    public Level[] levelData;
    public SpawnpointlistSO spawnpointlist;

    public Objective currentObjective;

    private void Start()
    {
        LevelEventSystem.instance.areaEntry += StartArea;
        // LevelEventSystem.instance.nextWave += StartWave;
        LevelEventSystem.instance.areaExit += FinsishArea;
        LevelEventSystem.instance.levelExit += FinishLevel;
    }
    private void OnDisable()
    {
        LevelEventSystem.instance.areaEntry -= StartArea;
        // LevelEventSystem.instance.nextWave -= StartWave;
        LevelEventSystem.instance.areaExit -= FinsishArea;
        LevelEventSystem.instance.levelExit -= FinishLevel;
    }

    private void Update()
    {
        if (currentObjective != null)
            currentObjective.ObjectiveUpdate(this);

    }

    public void StartLevel()
    {

    }

    public void StartArea()
    {
        GetNextObjective();
        if (HasNextObjective())
            currentArea++;
    }

    public void FinsishArea()
    {

    }

    public void FinishLevel()
    {

    }

    public void ExecuteCheckObjective()
    {

    }

    public void SwitchObjective()
    {
        levelData[currentLevel].areas[currentArea].StateExit(this);
        currentObjective = null;
    }

    Objective GetNextObjective()
    {
        if (HasNextObjective())
            return levelData[currentLevel].areas[currentArea + 1];
        else
            return null;
    }

    bool HasNextObjective()
    {
        return currentArea + 1 < levelData[currentLevel].areas.Length;
    }
}
