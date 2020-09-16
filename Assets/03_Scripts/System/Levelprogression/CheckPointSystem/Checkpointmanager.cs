﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpointmanager : MonoBehaviour
{
    CheckPoint currentCheck;
    CheckPoint oldCheck;
    void Start()
    {
        LevelEventSystem.instance.checkPointReached += SetNewCheckPoint;
        LevelEventSystem.instance.returnToCheckpoint += TransportToCheckpoint;
    }

    void SetNewCheckPoint(CheckPoint point)
    {
        currentCheck.isActive = false;
        oldCheck = currentCheck;
        currentCheck = point;
        currentCheck.isActive = true;
    }

    public void TransportToCheckpoint(PlayerBody body)
    {
        body.transform.position = currentCheck.transform.position;
    }

}
