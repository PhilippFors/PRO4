using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointList : MonoBehaviour
{
    public SpawnpointlistSO spawnlist;
    public AreaBarrierList barrierList;
    void Awake()
    {
        FindSpawnpoints();
        FindBarriers();
    }

    public void FindSpawnpoints()
    {
        spawnlist.list = new List<SpawnpointID>();
        SpawnpointID[] l = FindObjectsOfType<SpawnpointID>();
        foreach (SpawnpointID sp in l)
            spawnlist.list.Add(sp);
    }

    public void FindBarriers()
    {
        barrierList.list = new List<AreaBarrier>();
        AreaBarrier[] l = FindObjectsOfType<AreaBarrier>();
        foreach (AreaBarrier a in l)
            barrierList.list.Add(a);
    }

}
