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
    public Transform playerSpawn;
    [SerializeField] private Transform player;

    [Header("Level Data")]
    public LevelData[] levelData;

    public LevelData Arena;



    private void Start()
    {
        LevelEventSystem.instance.areaEntry += StartArea;
        LevelEventSystem.instance.areaExit += FinishArea;
        LevelEventSystem.instance.levelExit += FinishLevel;
        LevelEventSystem.instance.levelEntry += StartLevel;

        // StartLevel();
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
        currentObjective.ObjEnter(this);
        if (barrierList.list.Count > 0)
            foreach (AreaBarrier a in barrierList.list)
                if (a.AreaID == currentArea - 1)
                    a.Activate();
    }

    public void FinishArea()
    {
        if (barrierList.list.Count > 0)
            foreach (AreaBarrier a in barrierList.list)
            {
                if (a.AreaID == currentArea)
                    a.Deactivate();
                if (currentArea != 0)
                    if (a.AreaID == currentArea - 1)
                        a.Deactivate();
            }

        if (HasNextObjective())
            currentArea++;

        // StartCoroutine(SaveGame());
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
        player.GetComponent<PlayerStateMachine>().input.Gameplay.Disable();
        player.GetComponent<Animator>().applyRootMotion = false;

        StartCoroutine(WaitSpawn());
        // StartCoroutine(SaveGame());
        //TODO: Exit from Level transition
    }


    public void FindPlayerSpawnpoint()
    {
        foreach (SpawnPointWorker s in SpawnManager.instance.spawnpointlist.list)
            if (s.playerSpawnpoint)
            {
                playerSpawn = s.transform;
            }

        // SpawnManager.instance.spawnpointlist.list.Remove(playerSpawn.GetComponent<SpawnPointWorker>());
    }
    IEnumerator WaitSpawn()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        // player.position = playerSpawn.position;
        player.position = new Vector3(playerSpawn.position.x, playerSpawn.position.y, playerSpawn.position.z);
        yield return new WaitForEndOfFrame();
        player.GetComponent<Animator>().applyRootMotion = true;
        player.GetComponent<PlayerStateMachine>().input.Gameplay.Enable();
    }
    public void FinishLevel()
    {
        // GameManager.instance.currentLevel++;
        currentArea = 0;

        GameManager.instance.ReturnToStartMenu();

        //TODO: Start level transition
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
        if (GameManager.instance.arena)
            return Arena.areas[currentArea];
        else
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
