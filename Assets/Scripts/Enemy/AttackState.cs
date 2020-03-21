using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    Enemy _enemy;
    public AttackState(Enemy enemy) : base(enemy.gameObject)
    {
        _enemy = enemy;
    }

    public override void OnStateEnter()
    {
        return;
    }

    public override void OnStateExit()
    {
        return;
    }

    public override Type OnStateUpdate()
    {
        _enemy.Attack(1);

        if (Input.GetKeyDown("f"))
        {
            _enemy.StopAnim();
            return typeof(IdleState);
        }
        if (Vector3.Distance(_enemy.Target.position, transform.position) > EnemySettings.EnemyRange)
        {
            _enemy.StopAnim();
            return typeof(ChaseState);
        }
        return typeof(AttackState);
    }

}
