using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    // Start is called before the first frame update
    private Dictionary<Type, BaseState> _availableStates;

    public BaseState CurrentState;

    // Update is called once per frame
    void Update()
    {
        if (CurrentState == null)
        {
            
            CurrentState = _availableStates[typeof(IdleState)];
            CurrentState.OnStateEnter();
        }

        var nextState = CurrentState?.OnStateUpdate();

        if (nextState != null && nextState != CurrentState?.GetType())
        {
            SwitchToNewState(nextState);
        }
    }

    public void SetStates(Dictionary<Type, BaseState> states)
    {
        _availableStates = states;
    }

    private void SwitchToNewState(Type nextState)
    {
        CurrentState.OnStateExit();
        CurrentState = _availableStates[nextState];
        CurrentState.OnStateEnter();
    }
}
