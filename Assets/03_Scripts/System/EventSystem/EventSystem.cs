﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{
    public event Action<EnemyBaseClass, float> AttackEnemy;
    public event Action<PlayerBody, float> AttackPlayer;
    public event Action<DestructableObstacleBase, float> AttackObstacle;
    public event Action<MultiplierName, float, bool, float> ActivateSkill;

    public event Action GrenadeAim;

    private static EventSystem _instance;
    public static EventSystem instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("null");

            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    public void OnAttack(EnemyBaseClass enemy, float basedmg)
    {
        AttackEnemy(enemy, basedmg);
    }

    public void OnAttack(PlayerBody player, float basedmg)
    {
        AttackPlayer(player, basedmg);
    }

    public void OnAttack(DestructableObstacleBase obstacle, float basedmg)
    {
        AttackObstacle(obstacle, basedmg);
    }

    public void OnSkill(MultiplierName name, float value, bool SkillActivated, float Skilltime = 0f)
    {
        ActivateSkill(name, value, SkillActivated, Skilltime);
    }

    public void OnGrenadeAim()
    {
        GrenadeAim();
    }
}
