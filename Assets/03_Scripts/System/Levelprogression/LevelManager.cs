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
        currentObjective.ObjectiveUpdate(this);
    }

    public void StartLevel()
    {

    }

    public void StartArea()
    {
        
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

    public void SwitchObjective(){
        levelData[currentLevel].areas[currentArea].objective.StateExit(this);
        FinsishArea();
        currentObjective = GetNextObjective();
    }

    Objective GetNextObjective(){
        return null;
    }
}
