using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointList : MonoBehaviour
{
    public List<SpawnpointID> spawnList = new List<SpawnpointID>();
    void Start()
    {
        SendList();
    }

    public void FindSpawnpoints()
    {
        spawnList = new List<SpawnpointID>();
        SpawnpointID[] list = FindObjectsOfType<SpawnpointID>();
        foreach (SpawnpointID sp in list)
            spawnList.Add(sp);
    }

    public void SendList()
    {
        LevelEventSystem.instance.GetList(spawnList);
    }
}
