using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour, IStats
{
    public List<GameStatistics> statList { get; set; }

    public PlayerTemplate template;
    private void Awake()
    {
        InitStats();
    }
    public void InitStats()
    {
       statList = StatInit.InitPlayerStats(template);
    }

    public void SetStatValue(StatName name, float value)
    {
        statList.Find(x => x.GetName().Equals(name)).SetValue(value);
    }

    public float GetStatValue(StatName stat)
    {
        return statList.Find(x => x.GetName().Equals(stat)).GetValue();
    }

    public void CalculateHealth(float damage)
    {
        //float damage = baseDmg * (baseDmg/(baseDmg + enemy.GetStat(EnemyStatName.defense)))
        float newDamage = damage * damage / (damage + GetStatValue(StatName.defense));
        SetStatValue(StatName.health, GetStatValue(StatName.health) - damage);
        Debug.Log(gameObject.name + " just took " + newDamage + " damage.");
    }

    public void OnDeath()
    {
        //Death
    }
}
