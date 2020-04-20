using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;


public class ChaseState : BaseState
{
    private DurgaAI durgaAI;
    private EnemyBaseClass durgaSettings;
    public ChaseState(DurgaAI durga, EnemyBaseClass template) : base(durga.gameObject, template)
    {
        durgaAI = durga;
        durgaSettings = template;
    }

    public override void OnStateEnter()
    {
       // Vector3 dir = _Enemy_Prototype.Target.position - _Enemy_Prototype.transform.position;
       // dir.y = 0;
       // Quaternion look = Quaternion.LookRotation(dir);
       // transform.rotation = Quaternion.Lerp(_Enemy_Prototype.transform.rotation, look, Time.deltaTime * 2);
        return;
    }

    public override void OnStateExit()
    {
        return;
    }

    public override Type OnStateUpdate()
    {
        Vector3 dir = durgaAI.Target.position - durgaAI.transform.position;
        dir.y = 0;
        Quaternion look = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(durgaAI.transform.rotation, look, Time.deltaTime * durgaSettings.GetStatValue(StatName.turnSpeed));

        transform.position += transform.forward * durgaSettings.GetCalculatedValue(StatName.speed, MultiplierName.speed) * Time.deltaTime;
        //
        if (Vector3.Distance(durgaAI.Target.position, transform.position) < durgaSettings.GetStatValue(StatName.range))
        {
            return typeof(AttackState);
        }
        return typeof(ChaseState);
    }
}
