using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour, IStats, IMultipliers
{
    public List<GameStatistics> statList { get; set; }
    public List<Multiplier> multList { get; set; }
    public EnemyTemplate template;

    private void Awake()
    {
        InitStats();
        InitMultiplier();
    }
    public void CalculateHealth(float damage)
    {
        float calcDamage = damage * GetMultValue(MultiplierName.damage);
        // damage = damage * damage / (damage + (enemy.GetStatValue(StatName.defense) * MultiplierManager.instance.GetEnemyMultValue(MultiplierName.defense)));
        calcDamage = damage * (damage / (damage + (GetStatValue(StatName.defense) * GetMultValue(MultiplierName.defense))));
        SetStatValue(StatName.health, (GetStatValue(StatName.health) - calcDamage));
        Debug.Log(gameObject.name + " just took " + calcDamage + " damage.");
    }
    public virtual void OnDeath(){
        Destroy(gameObject);
    }
    public void SetMultValue(MultiplierName name, float value)
    {
        multList.Find(x => x.GetName().Equals(name)).SetValue(GetMultValue(name) + value);
    }
    public float GetMultValue(MultiplierName name)
    {
        return multList.Find(x => x.GetName().Equals(name)).GetValue();
    }

    public void ResetMultipliers()
    {
        foreach (Multiplier mult in multList)
        {
            mult.ResetMultiplier();
        }
    }

    public void SetStatValue(StatName name, float value)
    {
        statList.Find(x => x.GetName().Equals(name)).SetValue(value);
    }
    public float GetStatValue(StatName stat)
    {
        return statList.Find(x => x.GetName().Equals(stat)).GetValue();
    }

    public void InitStats()
    {
        statList = StatInit.InitEnemyStats(template);
    }

    public void InitMultiplier(){
        multList = StatInit.InitEnemyMultipliers();
    }

}
