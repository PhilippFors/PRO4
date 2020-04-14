using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineController : MonoBehaviour
{
    [HideInInspector] public EnemyBaseClass enemystats => GetComponent<EnemyBaseClass>();
    public State currentState;
    public State remainState;
    public State startState;
    public bool aiActive;
    public Transform target { get; private set; }
    [HideInInspector] public Transform self => GetComponent<Transform>();
    public Animator animator;

    void Start()
    {

    }

    void Update()
    {
        if (!aiActive)
            return;

        if (currentState == null)
            currentState = startState;

        currentState.StateUpdate(this);
    }

    public void SwitchStates(State nextState)
    {
        if (nextState != null && nextState != remainState)
        {
            currentState.StateExit(this);
            currentState = nextState;
            currentState.StateEnter(this);
        }
    }

    public void FindTarget()
    {
        target = FindObjectOfType<PlayerBody>().GetComponent<Transform>();
    }
}
