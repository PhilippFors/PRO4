using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LevelManager : MonoBehaviour
{
    public int currentLevel = 0;
    public int currentArea = 0;
    bool levelExitTriggered = false;
    public Level[] levelData;
    public SpawnpointlistSO spawnpointlist;
    public SpawnManager spawnManager;
    public Objective currentObjective;
    public float deltaTime;
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
        deltaTime = Time.deltaTime;
        if (currentObjective == null)
            return;

        currentObjective.ObjectiveUpdate(this);
    }

    public void StartArea()
    {
        currentObjective.started = true;
        currentObjective = GetNextObjective();
        currentObjective.ObjEnter(this);
    }

    public void FinsishArea()
    {
        if (HasNextObjective())
            currentArea++;
    }

    public void StartLevel()
    {

    }

    public void FinishLevel()
    {

    }

    public void SwitchObjective()
    {
        levelData[currentLevel].areas[currentArea].ObjExit(this);
        currentObjective.finished = true;
        currentObjective = null;
        FinsishArea();
    }

    Objective GetNextObjective()
    {
        // if (HasNextObjective())
        return levelData[currentLevel].areas[currentArea];
        // else
        // return null;
    }

    bool HasNextObjective()
    {
        return currentArea + 1 < levelData[currentLevel].areas.Length;
    }
}
