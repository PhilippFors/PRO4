using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    Enemy _enemy;
    float waitTime = 0;

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


        if (Input.GetKeyDown("f"))
        {
            _enemy.StopAnim();
            return typeof(IdleState);
        }
        if (Vector3.Distance(_enemy.Target.position, transform.position) < EnemySettings.EnemyRange)
        {
            _enemy.Attack(1);
        }
        else
        {
            _enemy.StopAnim();
            if (Wait())
            {
                return typeof(ChaseState);
            }
        }
        return typeof(AttackState);
    }
    bool Wait()
    {
        while (waitTime < 0.5f)
        {
            waitTime += Time.deltaTime;
            return false;
        }
        waitTime = 0f;
        return true;

    }
}
