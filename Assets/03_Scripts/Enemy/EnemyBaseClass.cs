using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBaseClass : MonoBehaviour
{
    protected List<EnemyStatistics> statList;

    private void Update()
    {
        MultUpdate();
    }
    public virtual void OnDeath()
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
        statList.Find(x => x.GetName().Equals(stat)).SetValue(value);
        if (stat == EnemyStatName.health)
            CheckHealth();
    }

    public float GetStat(EnemyStatName stat)
    {
        return statList.Find(x => x.GetName().Equals(stat)).GetValue();
    }

    void MultUpdate()
    {
        float speed = GetStat(EnemyStatName.speed);
        SetStat(EnemyStatName.speed, speed * MultiplierManager.instance.GetMultiplierValue(MultiplierName.speedMod));
    }
}
