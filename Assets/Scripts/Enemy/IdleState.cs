using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    private Enemy _enemy;

    public IdleState(Enemy enemy) : base(enemy.gameObject)
    {
        _enemy = enemy;
    }

    public override void OnStateEnter()
    {
        throw new NotImplementedException();
    }

    public override void OnStateExit()
    {
        throw new NotImplementedException();
    }

    public override Type OnStateUpdate()
    {
        throw new NotImplementedException();
    }
}
