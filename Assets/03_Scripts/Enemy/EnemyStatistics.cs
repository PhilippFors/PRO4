using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyStatName
{
    health,
    speed,
    range,
    turnSpeed,
    defense
}
public struct EnemyStatistics
{
    private float v;
    private EnemyStatName _name;
    public EnemyStatistics(float value, EnemyStatName name)
    {
        v = value;
        _name = name;
    }

    public void SetStat(float value)
    {
        v = value;
    }

    public float GetStat()
    {
        return v;
    }

    public EnemyStatName GetName()
    {
        return _name;
    }
}

