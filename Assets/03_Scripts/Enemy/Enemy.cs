using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform Target { get; private set; }
    public GameObject target;
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
            {typeof(IdleState), new IdleState(this)},
            {typeof(ChaseState), new ChaseState(this) },
            {typeof(AttackState), new AttackState(this) },
        };

        StateMachine.SetStates(states);
    }

    public void GetTarget(Transform target)
    {
        this.Target = target;
        this.target = target.gameObject;
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
