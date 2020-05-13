using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour, IStats, IMultipliers
{
    public List<GameStatistics> statList { get; set; }
    public List<Multiplier> multList { get; set; }
    public StatTemplate statTemplate;
    public StatTemplate multTemplate;
    public GameObject parent;
    [SerializeField] private float currentHealth;
    private void Awake()
    {
        InitStats();
        InitMultiplier();
    }
    #region health
    void CheckHealth()
    {
        if (GetStatValue(StatName.MaxHealth) <= 0)
        {
            OnDeath();
        }
    }
    public void CalculateHealth(float damage)
    {
        float calcDamage = damage * GetMultValue(MultiplierName.damage);
        calcDamage = damage * (damage / (damage + (GetStatValue(StatName.Defense) * GetMultValue(MultiplierName.defense))));
        SetStatValue(StatName.MaxHealth, (GetStatValue(StatName.MaxHealth) - calcDamage));
        CheckHealth();
        Debug.Log(gameObject.name + " just took " + calcDamage + " damage.");
        currentHealth -= calcDamage;
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
    public void SetStatValue(StatName stat, float value)
    {
        statList.Find(x => x.GetName().Equals(stat)).SetValue(value);
    }
    public float GetStatValue(StatName stat)
    {
        return statList.Find(x => x.GetName().Equals(stat)).GetValue();

    }
    #endregion

    #region Init
    public void InitStats()
    {
        foreach (FloatReference f in statTemplate.statList)
        {
            StatVariable s = (StatVariable)f.Variable;
            statList.Add(new GameStatistics(f.Value, s.statName));
        }
        currentHealth = GetStatValue(StatName.MaxHealth);
    }

    public void InitMultiplier()
    {
        foreach (FloatReference f in multTemplate.statList)
        {
            MultVariable s = (MultVariable)f.Variable;
            multList.Add(new Multiplier(f.Value, s.multiplierName));
        }
    }
    #endregion
}
