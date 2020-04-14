﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DurgaAI : MonoBehaviour
{
    public Transform Target { get; private set; }
    public DurgaBody durgaBody => GetComponent<DurgaBody>();
    public Animator animator;
    public EnemyStateMachine StateMachine => GetComponent<EnemyStateMachine>();

    private void Awake()
    {
        InitStates();

    }
    private void InitStates()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            {typeof(IdleState), new IdleState(this, durgaBody)},
            {typeof(ChaseState), new ChaseState(this, durgaBody) },
            {typeof(AttackState), new AttackState(this, durgaBody) },
        };

        StateMachine.SetStates(states);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, durgaBody.GetStatValue(StatName.range));
    }
    public void GetTarget(Transform target)
    {
        this.Target = target;
    }

    public void Attack(int attackSequenze)
    {
        animator.SetInteger("Sequenz1", attackSequenze);
    }

    public void StopAnim()
    {
        animator.SetTrigger("cancel");
    }
}
