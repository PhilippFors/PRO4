using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{


    private List<GameStatistics> statList;
    [SerializeField] private PlayerTemplate template;
    private void Start()
    {
        statList = StatInit.InitPlayerStats(template);
    }
    private void Update()
    {
        // MultUpdate();
    }
    protected virtual void OnDeath()
    {
        Debug.Log("I died!");
    }

    public void SetStatValue(StatName name, float value)
    {
        if (name == StatName.health)
        {
            float initHealth = statList.Find(x => x.GetName().Equals(name)).GetValue();
            statList.Find(x => x.GetName().Equals(name)).SetValue(initHealth - value);
            CheckHealth();
        }
        else
        {
            statList.Find(x => x.GetName().Equals(name)).SetValue(value);
        }
    }

    public float GetStatValue(StatName stat)
    {
        return statList.Find(x => x.GetName().Equals(stat)).GetValue();
    }

    // void MultUpdate()
    // {
    //     float speed = GetStatValue(StatName.speed);
    //     SetStatValue(StatName.speed, speed * MultiplierManager.instance.GetMultiplierValue(MultiplierName.speedMod));
    // }

    protected virtual void CheckHealth()
    {
        if (GetStatValue(StatName.health) <= 0)
        {
            Debug.Log("Player is dead");

        }
    }
}
