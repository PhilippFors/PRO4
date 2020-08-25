using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AStats : MonoBehaviour, IStats, IMultipliers
{
    public List<GameStatistics> statList { get; set; }
    public List<Multiplier> multList { get; set; }
    
    virtual public void InitStats(StatTemplate template)
    {
        multList = new List<Multiplier>();
        statList = new List<GameStatistics>();
        foreach (FloatReference f in template.statList)
        {
            StatVariable s = (StatVariable)f.Variable;
            statList.Add(new GameStatistics(f.Value, s.statName));
        }
    }
    virtual public void SetStatValue(StatName name, float value)
    {
        statList.Find(x => x.GetName().Equals(name)).SetValue(value);
    }
    virtual public float GetStatValue(StatName stat)
    {
        return statList.Find(x => x.GetName().Equals(stat)).GetValue();
    }

    virtual public void AddMultiplier(MultiplierName name, float value, float time)
    {
        multList.Add(new Multiplier(value, name));
        StartCoroutine(MultiplierTimer(time, multList.FindIndex(x => x.GetName().Equals(name))));
    }
    virtual public float GetMultValue(MultiplierName name)
    {
        float value = 1;
        if (multList.Exists(x => x.GetName().Equals(name)))
        {
            List<Multiplier> list = multList.FindAll(x => x.GetName().Equals(name));
            foreach (Multiplier mult in list)
            {
                value += mult.GetValue();
            }
            return value;
        }
        else
        {
            return 1f;
        }
    }

    virtual public void CheckHealth()
    {

    }
    virtual public void ResetMultipliers()
    {
        multList.Clear();
    }

    virtual public IEnumerator MultiplierTimer(float time, int id)
    {
        yield return new WaitForSeconds(time);
        multList.RemoveAt(id);
    }

    virtual public void TakeDamage(float damage)
    {

    }

    virtual public void Heal(float healAmount)
    {

    }

    virtual public void OnDeath()
    {

    }
}
