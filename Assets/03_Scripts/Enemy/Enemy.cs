using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform Target { get; private set; }

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
    //private void OnDrawGizmosSelected()
    //{
        //Gizmos.DrawWireSphere(transform.position, EnemySettings.EnemyRange);
    //}
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
