using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : AStats
{
    // public List<GameStatistics> statList { get; set; }
    // public List<Multiplier> multList { get; set; }
    public StatTemplate template;
    public FloatVariable currentHealth;
    private void Awake()
    {
        this.InitStats(template);
    }
    public override void InitStats(StatTemplate template)
    {
        multList = new List<Multiplier>();
        statList = new List<GameStatistics>();
        foreach (FloatReference f in template.statList)
        {
            StatVariable s = (StatVariable)f.Variable;
            statList.Add(new GameStatistics(f.Value, s.statName));
        }
        currentHealth.Value = GetStatValue(StatName.MaxHealth);
    }

    // public void InitMultiplier()
    // {
    //     multList = new List<Multiplier>();
    //     foreach (FloatReference f in multTemplate.statList)
    //     {
    //         MultVariable s = (MultVariable)f.Variable;
    //         multList.Add(new Multiplier(f.Value, s.multiplierName));
    //     }
    // }

    // public void SetStatValue(StatName name, float value)
    // {
    //     statList.Find(x => x.GetName().Equals(name)).SetValue(value);
    // }

    // public float GetStatValue(StatName stat)
    // {
    //     return statList.Find(x => x.GetName().Equals(stat)).GetValue();
    // }

    public override void TakeDamage(float damage)
    {
        //float damage = baseDmg * (baseDmg/(baseDmg + enemy.GetStat(EnemyStatName.defense)))
        float newDamage = damage * damage / (damage + GetStatValue(StatName.Defense));
        currentHealth.Value -= newDamage;
        // SetStatValue(StatName.MaxHealth, GetStatValue(StatName.MaxHealth) - damage);
        Debug.Log(gameObject.name + " just took " + newDamage + " damage.");
    }

    public override void OnDeath()
    {
        //Death
    }

    public override void Heal(float healAmount)
    {
        float MaxHealth = GetStatValue(StatName.MaxHealth);
        if (currentHealth.Value < MaxHealth)
        {
            if (currentHealth.Value + healAmount > MaxHealth)
            {
                float overflow = currentHealth.Value + healAmount - MaxHealth;
                currentHealth.Value += healAmount - overflow;
            }
            else
            {
                currentHealth.Value += healAmount;
            }
        }
    }

    // public void AddMultiplier(MultiplierName name, float value, float time)
    // {
    //     multList.Add(new Multiplier(value, name));
    //     StartCoroutine(MultiplierTimer(time, multList.FindIndex(x => x.GetName().Equals(name))));
    //     // multList.Find(x => x.GetName().Equals(name)).SetValue(GetMultValue(name) + value);
    // }
    // public float GetMultValue(MultiplierName name)
    // {
    //     float value = 0;
    //     if (multList.Exists(x => x.GetName().Equals(name)))
    //     {
    //         List<Multiplier> list = multList.FindAll(x => x.GetName().Equals(name));
    //         foreach (Multiplier mult in list)
    //         {
    //             value += mult.GetValue();
    //         }
    //         return value;
    //     }
    //     else
    //     {
    //         return 1f;
    //     }
    // }

    // public IEnumerator MultiplierTimer(float time, int id)
    // {
    //     yield return new WaitForSeconds(time);
    //     multList.RemoveAt(id);
    // }

    // public void ResetMultipliers()
    // {
    //     foreach (Multiplier mult in multList)
    //         mult.ResetMultiplier();
    // }
}
