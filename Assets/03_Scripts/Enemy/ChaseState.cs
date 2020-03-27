using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;


public class ChaseState : BaseState
{
    private Enemy _enemy;

    public ChaseState(Enemy enemy) : base(enemy.gameObject)
    {
        _enemy = enemy;
    }

    public override void OnStateEnter()
    {
        Vector3 dir = _enemy.Target.position - _enemy.transform.position;
        dir.y = 0;
        Quaternion look = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(_enemy.transform.rotation, look, Time.deltaTime * 2);
        return;
    }

    public override void OnStateExit()
    {
        return;
    }

    public override Type OnStateUpdate()
    {
        Vector3 dir = _enemy.Target.position - _enemy.transform.position;
        dir.y = 0;
        Quaternion look = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(_enemy.transform.rotation, look, Time.deltaTime * EnemySettings.EnemyLookSpeed);

        transform.position += transform.forward * EnemySettings.EnemySpeed * Time.deltaTime;
        //
        if (Vector3.Distance(_enemy.Target.position, transform.position) < EnemySettings.EnemyRange)
        {
            return typeof(AttackState);
        }
        return typeof(ChaseState);
    }
}
