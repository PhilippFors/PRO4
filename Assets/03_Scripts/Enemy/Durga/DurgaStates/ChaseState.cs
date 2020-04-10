using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;


public class ChaseState : BaseState
{
    private DurgaAI durgaAI;
    private IEnemyBase durgaSettings;
    public ChaseState(DurgaAI durga, IEnemyBase template) : base(durga.gameObject, template)
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
        transform.rotation = Quaternion.Lerp(durgaAI.transform.rotation, look, Time.deltaTime * durgaSettings.getTurnSpeed());

        transform.position += transform.forward * durgaSettings.getSpeed() * Time.deltaTime;
        //
        if (Vector3.Distance(durgaAI.Target.position, transform.position) < durgaSettings.getRange())
        {
            return typeof(AttackState);
        }
        return typeof(ChaseState);
    }
}
