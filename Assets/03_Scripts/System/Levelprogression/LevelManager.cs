using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LevelManager : MonoBehaviour
{
    public int currentLevel = 0;
    public int currentArea = 0;
    bool levelExitTriggered = false;
    public Objective currentObjective;
    public AreaBarrierList barrierList;
    [HideInInspector] public float deltaTime;

    [Header("Level Data")]
    public Level[] levelData;
    Transform playerSpawnpoint;
    [SerializeField] private Transform player;
    private void Start()
    {
        LevelEventSystem.instance.areaEntry += StartArea;
        LevelEventSystem.instance.areaExit += FinishArea;
        LevelEventSystem.instance.levelExit += FinishLevel;
        LevelEventSystem.instance.levelEntry += StartLevel;
    }

    private void OnDisable()
    {
        LevelEventSystem.instance.areaEntry -= StartArea;
        LevelEventSystem.instance.areaExit -= FinishArea;
        LevelEventSystem.instance.levelExit -= FinishLevel;
        LevelEventSystem.instance.levelEntry -= StartLevel;
    }

    private void Update()
    {
        deltaTime = Time.deltaTime;
        if (currentObjective != null)
            currentObjective.ObjectiveUpdate(this);
    }

    public void StartArea()
    {
        currentObjective = GetNextObjective();
        currentObjective.started = true;
        currentObjective.AutoEnter(this);
        currentObjective.ObjEnter(this);
    }

    public void FinishArea()
    {
        foreach (AreaBarrier a in barrierList.list)
            if (a.AreaID == currentArea)
                a.Deactivate();

        if (HasNextObjective())
            currentArea++;

        StartCoroutine(SaveGame());
    }

    public void CheckEndofArea()
    {
        if (currentArea + 1 < levelData[currentLevel].areas.Length)
        {
            if (levelData[currentLevel].areas[currentArea + 1].AreaID != levelData[currentLevel].areas[currentArea].AreaID)
                FinishArea();
        }
        else if (currentArea + 1 >= levelData[currentLevel].areas.Length)
        {
            FinishArea();
        }
    }

    public void StartLevel()
    {
        FindPlayerSpawnpoint();
        player.position = playerSpawnpoint.position;
        StartCoroutine(SaveGame());
        //TODO: Exit from Level transition
    }


    public void FindPlayerSpawnpoint()
    {
        foreach (SpawnpointID s in SpawnManager.instance.spawnpointlist.list)
            if (s.playerSpawnpoint)
            {
                playerSpawnpoint = s.transform;
            }

        SpawnManager.instance.spawnpointlist.list.Remove(playerSpawnpoint.GetComponent<SpawnpointID>());
    }

    public void FinishLevel()
    {
        //TODO: Start loading new level
        //TODO: Start level transition
        //TODO: Reset Area count
    }

    public void SwitchObjective()
    {
        CheckEndofArea();
        levelData[currentLevel].areas[currentArea].ObjExit(this);
        currentObjective.finished = true;
        currentObjective = null;
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

    public void SetArea(int a)
    {
        currentArea = a;
    }

    public void SetLevel(int a)
    {
        currentLevel = a;
    }

    IEnumerator SaveGame()
    {
        yield return new WaitForEndOfFrame();
        SaveManager.instance.Save();
    }

}
