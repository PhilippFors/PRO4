using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour, IStats, IMultipliers
{
    public List<GameStatistics> statList { get; set; }
    public List<Multiplier> multList { get; set; }
    public EnemyTemplate template;

    public GameObject parent;

    private void Awake()
    {
        InitStats();
        InitMultiplier();
    }
    #region health
    void CheckHealth(){
        if(GetStatValue(StatName.health)<= 0){
            OnDeath();
        }
    }
    public void CalculateHealth(float damage)
    {
        float calcDamage = damage * GetMultValue(MultiplierName.damage);
        calcDamage = damage * (damage / (damage + (GetStatValue(StatName.defense) * GetMultValue(MultiplierName.defense))));
        SetStatValue(StatName.health, (GetStatValue(StatName.health) - calcDamage));
        CheckHealth();
        Debug.Log(gameObject.name + " just took " + calcDamage + " damage.");

        // damage = damage * damage / (damage + (enemy.GetStatValue(StatName.defense) * MultiplierManager.instance.GetEnemyMultValue(MultiplierName.defense)));
    }

    public void OnDeath()
    {
        EventSystem.instance.OnEnemyDeath(this);
        Destroy(parent);
    }
    #endregion

    #region Multipliers 
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
    #endregion

    #region Stats
    public void SetStatValue(StatName name, float value)
    {
        statList.Find(x => x.GetName().Equals(name)).SetValue(value);
    }
    public float GetStatValue(StatName stat)
    {
        return statList.Find(x => x.GetName().Equals(stat)).GetValue();
    }
    #endregion

    #region Init
    public void InitStats()
    {
        statList = StatInit.InitEnemyStats(template);
    }

    public void InitMultiplier()
    {
        multList = StatInit.InitEnemyMultipliers();
    }
    #endregion
}
