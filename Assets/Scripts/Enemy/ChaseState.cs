using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ChaseState : BaseState
{
    private Enemy _enemy;

    public ChaseState(Enemy enemy) : base(enemy.gameObject)
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
       transform.LookAt(new Vector3(_enemy.Target.position.x, transform.position.y, _enemy.Target.position.z));
       transform.position += transform.forward * EnemySettings.EnemySpeed * Time.deltaTime;
       //
       if(Vector3.Distance(_enemy.Target.position, transform.position) < EnemySettings.EnemyRange)
       {
           return typeof(AttackState);
       }
        return typeof(ChaseState);
    }



}
