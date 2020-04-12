using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperClass : MonoBehaviour
{
    protected List<GameStatistics> list;

    public void SetStat(StatName name, float value)
    {
        list.Find(x => x.GetName().Equals(name)).SetValue(value);
        
        if (name == StatName.health)
            CheckHealth();
    }

    public float getValue(StatName name){
        return list.Find(x => x.GetName().Equals(name)).GetValue();
    }

    private void Update()
    {
        MultUpdate();
    }
    protected void MultUpdate()
    {
        Debug.Log(list.Find(x => x.GetName().Equals(StatName.health)).GetValue() + ", " + this.gameObject.name );
        float speed = getValue(StatName.speed);
        SetStat(StatName.speed, speed * MultiplierManager.instance.GetEnemyMultValue(MultiplierName.speedMod));
    }
    
    protected virtual void OnDeath(){}

    protected void CheckHealth()
    {
        if (getValue(StatName.health) <= 0.0f)
        {
            OnDeath();
            Destroy(this.gameObject);
        }
    }
}
