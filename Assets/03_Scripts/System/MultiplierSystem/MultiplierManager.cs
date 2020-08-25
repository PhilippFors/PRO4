using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MultiplierManager : MonoBehaviour
{
    //Singleton setup
    public EnemySet set;
    public static MultiplierManager instance;
    private void OnDisable()
    {
        EventSystem.instance.ActivateSkill -= SetAllMultValues;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        EventSystem.instance.ActivateSkill += SetAllMultValues;
    }

    //TODO: Add Symbol to parameters, iterate over each enemy and compare Symbol names
    public void SetAllMultValues(Skills skill)
    {
        foreach (EnemyBody enemy in set.entityList)
        {
            if (enemy.symbolInfo.name.Equals(skill.symbol.name))
                if (skill.symbol.enhance)
                    enemy.AddMultiplier(skill.symbol.main, skill.increaseMultValue, skill.timer);
                else
                    enemy.AddMultiplier(skill.symbol.main, skill.decreaseMultValue, skill.timer);
        }
    }

    public void ResetMultiplier()
    {
        //iterate over every mod with foreach
        foreach (EnemyBody enemy in set.entityList)
        {
            enemy.ResetMultipliers();
        }
    }
}
