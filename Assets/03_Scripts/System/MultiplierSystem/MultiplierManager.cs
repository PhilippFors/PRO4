using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MultiplierManager : MonoBehaviour
{
    //Singleton setup
    public EnemySet set;
    public static MultiplierManager instance;
    public PlayerBody player;
    private void OnDisable()
    {
        MyEventSystem.instance.ActivateSkill -= SetEnemyMultValues;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        MyEventSystem.instance.ActivateSkill += SetEnemyMultValues;
    }

    //TODO: Add Symbol to parameters, iterate over each enemy and compare Symbol names
    public void SetEnemyMultValues(Skills skill)
    {
        foreach (EnemyBody enemy in set.entityList)
        {
            if (enemy.symbolInfo.name.Equals(skill.buffSymbol.name))
            {
                enemy.AddMultiplier(skill.buffSymbol.buff, skill.increaseMultValue, skill.timer);
                enemy.BuffAnim();
            }
            else if (enemy.symbolInfo.name.Equals(skill.debuffSymbol.name))
            {
                enemy.AddMultiplier(skill.debuffSymbol.debuff, skill.decreaseMultValue, skill.timer);
                enemy.DebuffAnim();
            }

        }
    }

    public void SetPlayerMultValues(MultiplierName name, float value, float time)
    {
        player.AddMultiplier(name, value, time);
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
