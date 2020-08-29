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

    public static LevelEventSystem instance;

    private void Awake()
    {
        instance = this;
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
