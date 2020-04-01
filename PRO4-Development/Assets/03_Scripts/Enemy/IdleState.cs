using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    private Enemy _enemy;
    public GameObject player;
    public IdleState(Enemy enemy) : base(enemy.gameObject)
    {
        _enemy = enemy;
    }

    public override void OnStateEnter()
    {
        player = GameObject.Find("player");
        _enemy.GetTarget(player.transform);
    }

    public override void OnStateExit()
    {
        return;
    }

    public override Type OnStateUpdate()
    {
        if(player != null)
        {
            return typeof(ChaseState);
        }
        return typeof(IdleState);
    }
}
