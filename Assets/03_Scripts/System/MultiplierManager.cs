using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MultiplierManager : MonoBehaviour
{
    //Singleton setup
    public static MultiplierManager instance;
    private void OnDisable()
    {
        EventSystem.instance.ActivateSkill -= SetAllMultValues;
        EventSystem.instance.ResetMult -= ResetMultiplier;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        EventSystem.instance.ActivateSkill += SetAllMultValues;
        EventSystem.instance.ResetMult += ResetMultiplier;
    }

    public void SetAllMultValues(MultiplierName multiplierName, float value)
    {
        foreach (EnemyBody enemy in SpawnManager.instance.enemyCollection)
        {
            enemy.SetMultValue(multiplierName, value);
        }
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
        foreach (EnemyBody enemy in SpawnManager.instance.enemyCollection)
        {
            enemy.ResetMultipliers();
        }
    }
}
