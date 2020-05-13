using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour, IStats
{
    public List<GameStatistics> statList { get; set; }
    public StatTemplate template;
    private void Awake()
    {
        InitStats();
    }
    public void InitStats()
    {
        foreach(FloatReference f in template.statList){
            StatVariable s = (StatVariable)f.Variable;
            statList.Add(new GameStatistics(f.Value, s.statName));
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

    public void CalculateHealth(float damage)
    {
        //float damage = baseDmg * (baseDmg/(baseDmg + enemy.GetStat(EnemyStatName.defense)))
        float newDamage = damage * damage / (damage + GetStatValue(StatName.Defense));
        SetStatValue(StatName.MaxHealth, GetStatValue(StatName.MaxHealth) - damage);
        Debug.Log(gameObject.name + " just took " + newDamage + " damage.");
    }

    public void OnDeath()
    {
        //Death
    }
}
