using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    DurgaAI durgaAI;
    EnemyBaseClass durgaSettings;
    float waitTime = 0;

    public AttackState(DurgaAI durga, EnemyBaseClass template) : base(durga.gameObject, template)
    {
        durgaAI = durga;
        durgaSettings = template;
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
            durgaAI.StopAnim();
            return typeof(IdleState);
        }
        if (Vector3.Distance(durgaAI.Target.position, transform.position) < durgaSettings.GetStat(EnemyStatName.range))
        {
            durgaAI.Attack(1);
        }
        else
        {
            durgaAI.StopAnim();
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
