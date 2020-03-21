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
        throw new NotImplementedException();
    }

    public override void OnStateExit()
    {
        throw new NotImplementedException();
    }

    public override Type OnStateUpdate()
    {
        return null;
    }
}
