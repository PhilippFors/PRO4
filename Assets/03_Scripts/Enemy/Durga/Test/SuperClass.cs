using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperClass : MonoBehaviour
{
    protected List<EnemyStatistics> list;

    public void SetStat(EnemyStatName name, float value)
    {
        list.Find(x => x.GetName().Equals(name)).SetValue(value);
        
        if (name == EnemyStatName.health)
            CheckHealth();
    }

    public float getValue(EnemyStatName name){
        return list.Find(x => x.GetName().Equals(name)).GetValue();
    }

    private void Update()
    {
        MultUpdate();
    }
    protected void MultUpdate()
    {
        Debug.Log(list.Find(x => x.GetName().Equals(EnemyStatName.health)).GetValue() + ", " + this.gameObject.name );
        float speed = getValue(EnemyStatName.speed);
        SetStat(EnemyStatName.speed, speed * MultiplierManager.instance.GetMultiplierValue(MultiplierName.speedMod));
    }
    
    protected virtual void OnDeath(){}

    protected void CheckHealth()
    {
        if (getValue(EnemyStatName.health) <= 0.0f)
        {
            OnDeath();
            Destroy(this.gameObject);
        }
    }
}
