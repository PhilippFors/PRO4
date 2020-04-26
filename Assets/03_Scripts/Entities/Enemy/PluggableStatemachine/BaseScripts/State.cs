using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/State")]
public class State : ScriptableObject
{
    public Action[] actions;
    public Transition[] transitions;

    public OnExitState onExitState;
    public OnEnterState onEnterState;

    public void StateUpdate(StateMachineController controller)
    {
        ExecuteActions(controller);

        CheckTransitions(controller);
    }

    private void ExecuteActions(StateMachineController controller)
    {
        foreach (Action action in actions)
        {
            action.Execute(controller);
        }
    }

    private void CheckTransitions(StateMachineController controller)
    {
        foreach (Transition transition in transitions)
        {
            if (transition.decision.Execute(controller))
            {
                controller.SwitchStates(transition.trueState);
            }
            else
            {
                controller.SwitchStates(transition.falseState);
            }

        }

    }
    public void StateExit(StateMachineController controller)
    {
        if (onExitState == null)
            return;

        onExitState.Execute(controller);
    }

    public void StateEnter(StateMachineController controller)
    {
        if (onEnterState == null)
            return;

        onEnterState.Execute(controller);
    }
}
