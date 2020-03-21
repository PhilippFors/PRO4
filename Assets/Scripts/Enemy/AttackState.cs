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
