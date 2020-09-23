using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : AStats, IHasHealth
{
    public StatTemplate template;
    public FloatVariable currentHealth;

    public bool alive = true;

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

    void CheckHealth()
    {
        if (currentHealth.Value <= 0)
        {
            OnDeath();
        }
    }

    public void TakeDamage(float damage)
    {
        //float damage = baseDmg * (baseDmg/(baseDmg + enemy.GetStat(EnemyStatName.defense)))
        float newDamage = damage * damage / (damage + GetStatValue(StatName.Defense));
        currentHealth.Value -= newDamage;
        // SetStatValue(StatName.MaxHealth, GetStatValue(StatName.MaxHealth) - damage);
        Debug.Log(gameObject.name + " just took " + newDamage + " damage.");
        CheckHealth();
    }

    public void OnDeath()
    {
        alive = false;
        LevelEventSystem.instance.ReturnToCheckpoint(this);
    }

    public void Heal(float healAmount)
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
}
