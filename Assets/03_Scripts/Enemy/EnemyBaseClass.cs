﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBaseClass : MonoBehaviour
{
    protected List<GameStatistics> statList;
    protected float deathTimer;
    private void Update()
    {
        MultUpdate();
    }
    protected virtual void OnDeath()
    {
        Debug.Log("I died!");
    }

    protected void Kill()
    {
        Destroy(this.gameObject);
    }

    protected virtual void CheckHealth()
    {
        if (GetStatValue(StatName.health) <= 0)
        {
            OnDeath();
            Invoke("Kill", deathTimer);
        }
    }
    public void SetStatValue(StatName name, float value)
    {

        if (name == StatName.health)
        {
            float initHealth = statList.Find(x => x.GetName().Equals(name)).GetValue();
            statList.Find(x => x.GetName().Equals(name)).SetValue(initHealth - value);
            CheckHealth();
        }
        else
        {
            statList.Find(x => x.GetName().Equals(name)).SetValue(value);
        }
    }

    public float GetStatValue(StatName stat)
    {
        return statList.Find(x => x.GetName().Equals(stat)).GetValue();
    }

    void MultUpdate()
    {
        float speed = GetStatValue(StatName.speed);
        SetStatValue(StatName.speed, speed * MultiplierManager.instance.GetEnemyMultValue(MultiplierName.speedMod));
    }
}
