using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelEventSystem : MonoBehaviour
{
    public event System.Action areaEntry;
    public event System.Action areaExit;
    public event System.Action levelEntry;
    public event System.Action levelExit;

    public event Action<CheckPoint> checkPointReached;
    public event Action<PlayerBody> returnToCheckpoint;
    public static LevelEventSystem instance;
    

    private void Awake()
    {
        instance = this;
    }

    public void ReturnToCheckpoint(PlayerBody player){
        if(returnToCheckpoint != null)
            returnToCheckpoint(player);
    }
    public void CheckPoinReached(CheckPoint point)
    {
        if (checkPointReached != null)
            checkPointReached(point);
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
}
