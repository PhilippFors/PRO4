using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    private DurgaAI durgaAI;
    public GameObject player;
    EnemyBaseClass durgaSettings;
    public IdleState(DurgaAI durga, EnemyBaseClass template) : base(durga.gameObject, template)
    {
        durgaAI = durga;
        durgaSettings = template;
    }

    public override void OnStateEnter()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        durgaAI.GetTarget(player.transform);
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
