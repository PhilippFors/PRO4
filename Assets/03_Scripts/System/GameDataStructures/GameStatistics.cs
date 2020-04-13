using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StatName
{
    undefined,
    health,
    speed,
    range,
    turnSpeed,
    defense,
    attackSpeed
}
public class GameStatistics
{
    private float v;
    private StatName _name;

    public GameStatistics(float value, StatName name){
        v = value;
        _name = name;

    }

    public void SetValue(float value)
    {
        v = value;
    }

    public float GetValue()
    {
        return v;
    }

    public StatName GetName()
    {
        return _name;
    }
}

