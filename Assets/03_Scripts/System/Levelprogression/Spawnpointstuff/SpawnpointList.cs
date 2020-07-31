using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointList : MonoBehaviour
{
    public SpawnpointlistSO spawnlist;
    void Start()
    {
        FindSpawnpoints();
    }

    public void FindSpawnpoints()
    {
        spawnlist.list = new List<SpawnpointID>();
        SpawnpointID[] l = FindObjectsOfType<SpawnpointID>();
        foreach (SpawnpointID sp in l)
            spawnlist.list.Add(sp);
    }

}
