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
        Debug.Log("Player died!");
    }

    public void SetStatValue(StatName name, float value)
    {
        statList.Find(x => x.GetName().Equals(name)).SetValue(value);
        if (name == StatName.health)
        {
            Debug.Log(gameObject.name + " just took " + value + " damage");
            CheckHealth();
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
            OnDeath();

        }
    }
}
