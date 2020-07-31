using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelEventSystem : MonoBehaviour
{
    public event System.Action areaEntry;
    public event System.Action areaExit;
    public event System.Action nextWave;
    public event System.Action levelEntry;
    public event System.Action levelExit;
    public event Action<List<SpawnpointID>> getList;

    public static LevelEventSystem instance;

    private void Awake()
    {
        instance = this;
    }

    public void GetList(List<SpawnpointID> l)
    {
        if (getList != null)
            getList(l);
    }

    public void AreaEntry()
    {
        if (areaEntry != null)
            areaEntry();
    }
    public void AreaExit()
    {
        if (areaExit != null)
            areaExit();
    }
    public void LevelEntry()
    {
        if (levelEntry != null)
            levelEntry();
    }
    public void LevelExit()
    {
        if (levelExit != null)
            levelExit();
    }

    public void NextWave()
    {
        if (nextWave != null)
        {
            nextWave();
        }
    }


}
