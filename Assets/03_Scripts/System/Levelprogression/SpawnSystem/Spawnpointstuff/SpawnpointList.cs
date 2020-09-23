using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointList : MonoBehaviour
{
    public SpawnpointlistSO spawnlist;
    public AreaBarrierList barrierList;
    void OnEnable()
    {
        FindSpawnpoints();
        FindBarriers();
    }

    public void FindSpawnpoints()
    {
        spawnlist.list = new List<SpawnPointWorker>();
        SpawnPointWorker[] l = FindObjectsOfType<SpawnPointWorker>();
        foreach (SpawnPointWorker sp in l)
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
