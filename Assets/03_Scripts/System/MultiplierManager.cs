using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MultiplierManager : MonoBehaviour
{
    //Lists are dynamic in size. Array would technically work too.
    private List<Multiplier> playerMultList = new List<Multiplier>();

    //Singleton setup
    private static MultiplierManager _instance;
    public static MultiplierManager instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("null");

            return _instance;
        }
    }

    private void OnEnable()
    {
        EventSystem.instance.ActivateSkill += SetAllMultValues;
        EventSystem.instance.ResetMult += ResetMultiplier;
    }
    
    private void OnDisable()
    {
        EventSystem.instance.ActivateSkill -= SetAllMultValues;
        EventSystem.instance.ResetMult -= ResetMultiplier;
    }
    

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        playerMultList = StatInit.InitPlayerMultipliers();
    }

    public void SetAllMultValues(MultiplierName multiplierName, float value)
    {
        foreach (EnemyBaseClass enemy in SpawnManager.instance.enemies)
        {
            enemy.SetMultValue(multiplierName, value);
        }

    }

    // public void SetSingleMultiplier(EnemyBaseClass enemy, MultiplierName name, float value)
    // {
    //     enemy.SetMultValue(name, value);
    // }

    // public float GetSingleMultiplier(EnemyBaseClass enemy, MultiplierName name)
    // {
    //     return enemy.GetMultValue(name);
    // }
    public float GetPlayerMultValue(MultiplierName name)
    {
        if (playerMultList.Exists(x => x.GetName().Equals(name)))
            return playerMultList.Find(x => x.GetName().Equals(name)).GetValue() == 0 ? 1.0f : playerMultList.Find(x => x.GetName().Equals(name)).GetValue();

        return 1;
    }

    public void SetPlayerMultValue(MultiplierName name, float value)
    {
        playerMultList.Find(x => x.GetName().Equals(name)).SetValue(value);
    }

    IEnumerator ResetTimer(float Skilltime)
    {
        yield return new WaitForSeconds(Skilltime);
        ResetMultiplier();
        //Reset Music
        yield return null;
    }

    public void ResetMultiplier()
    {
        //iterate over every mod with foreach
        foreach (EnemyBaseClass enemy in SpawnManager.instance.enemies)
        {
            enemy.ResetMultipliers();
        }
    }
}
