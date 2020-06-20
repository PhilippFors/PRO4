using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyBody : MonoBehaviour, IStats, IMultipliers
{
    public List<GameStatistics> statList { get; set; }
    public List<Multiplier> multList { get; set; }
    public StatTemplate statTemplate;
    public StatTemplate multTemplate;
    public GameObject parent;
    public EnemySet set;
    [SerializeField] private float currentHealth;
    private void Awake()
    {
        InitStats();
        InitMultiplier();
    }

    #region Init
    public void InitStats()
    {
        statList = new List<GameStatistics>();
        foreach (FloatReference f in statTemplate.statList)
        {
            StatVariable s = (StatVariable)f.Variable;
            statList.Add(new GameStatistics(f.Value, s.statName));
        }

        currentHealth = GetStatValue(StatName.MaxHealth);
    }

    public void InitMultiplier()
    {
        multList = new List<Multiplier>();
        foreach (FloatReference f in multTemplate.statList)
        {
            MultVariable s = (MultVariable)f.Variable;
            multList.Add(new Multiplier(f.Value, s.multiplierName));
        }
    }


    #endregion
    #region health
    void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }
    public void TakeDamage(float damage)
    {
        float calcDamage = damage * GetMultValue(MultiplierName.damage);
        calcDamage = damage * (damage / (damage + (GetStatValue(StatName.Defense) * GetMultValue(MultiplierName.defense))));
        // SetStatValue(StatName.MaxHealth, (GetStatValue(StatName.MaxHealth) - calcDamage));
        
        Debug.Log(gameObject.name + " just took " + calcDamage + " damage.");
        currentHealth -= calcDamage;
        CheckHealth();
        // damage = damage * damage / (damage + (enemy.GetStatValue(StatName.defense) * MultiplierManager.instance.GetEnemyMultValue(MultiplierName.defense)));
    }

    public void Heal(float healAmount)
    {

    }
    public void OnDeath()
    {
        EventSystem.instance.OnEnemyDeath(this);
        Destroy(this.gameObject.GetComponentInParent<Transform>());
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
    public void SetStatValue(StatName stat, float value)
    {
        statList.Find(x => x.GetName().Equals(stat)).SetValue(value);
        
    }
    public float GetStatValue(StatName stat)
    {
        return statList.Find(x => x.GetName().Equals(stat)).GetValue();

    }
    #endregion
}
