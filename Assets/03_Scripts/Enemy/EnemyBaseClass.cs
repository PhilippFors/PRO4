using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBaseClass : MonoBehaviour
{
    protected List<GameStatistics> statList;
    protected List<Multiplier> multList;
    protected float deathTimer;

    protected virtual void OnDeath()
    {
        Debug.Log("I died!");
    }

    protected void Kill()
    {
        Destroy(this.gameObject);
    }

    protected virtual void CheckHealth()
    {
        if (GetStatValue(StatName.health) <= 0)
        {
            OnDeath();
            Invoke("Kill", deathTimer);
        }
    }
    public void SetStatValue(StatName name, float value)
    {
        statList.Find(x => x.GetName().Equals(name)).SetValue(value);
        
        if (name == StatName.health)
        {
            Debug.Log(gameObject.name + " just took " + value + " damage.");
            CheckHealth();
        }

    }

    public float GetStatValue(StatName stat)
    {
        return statList.Find(x => x.GetName().Equals(stat)).GetValue();
    }
    public void SetMultValue(MultiplierName name, float value){
        multList.Find(x => x.GetName().Equals(name)).SetValue(GetMultValue(name) + value);
    }

    public float GetMultValue(MultiplierName name){
        return multList.Find(x => x.GetName().Equals(name)).GetValue();
    }

    public float GetCalculatedValue(StatName stat, MultiplierName mult){
        return GetStatValue(stat) * GetMultValue(mult);
    }

    public void ResetMultipliers(){
        foreach (Multiplier mult in multList){
            mult.ResetMultiplier();
        }
    }

}
