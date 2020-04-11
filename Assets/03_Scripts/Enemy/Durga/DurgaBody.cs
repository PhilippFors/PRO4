using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DurgaBody : MonoBehaviour, IEnemyBase
{

    public EnemyTemplate durgaTemplate;
    public List<EnemyStatistics> statList = new List<EnemyStatistics>();
    private void Start()
    {
        statList = StatInit.InitEnemyStats(durgaTemplate);

        Debug.Log(GetStat(EnemyStatName.range));
    }

    private void Update()
    {
        ModUpdate();
    }
    void OnDeath()
    {
        Debug.Log("I died!");
    }

    void CheckHealth()
    {
        if (GetStat(EnemyStatName.health) <= 0)
        {
            OnDeath();
            Destroy(gameObject);
        }
    }

    public void SetStat(EnemyStatName stat, float value)
    {
        statList.Find(x => x.GetName().Equals(stat)).SetStat(value);
        if (stat == EnemyStatName.health)
            CheckHealth();
    }

    public float GetStat(EnemyStatName stat)
    {
        return statList.Find(x => x.GetName().Equals(stat)).GetStat();
    }

    void ModUpdate()
    {
        float speed = GetStat(EnemyStatName.speed);
        SetStat(EnemyStatName.speed, speed * MultiplierManager.instance.GetModValue(MultiplierName.speedMod));
    }
}
