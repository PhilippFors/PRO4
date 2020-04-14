using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/State")]
public class State : ScriptableObject
{
    public Action[] actions;
    public Transition[] transitions;

    public OnExitState exitState;
    public OnEnterState enterState;

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
            if (transition.decision.ExecuteDecision(controller))
            {
                controller.SwitchStates(transition.trueState);
            }
            else
            {
                controller.SwitchStates(transition.falseState);
            }

        }

    }
    public virtual void StateExit(StateMachineController controller)
    {
        if (exitState == null)
            return;

        exitState.Execute(controller);
    }

    public virtual void StateEnter(StateMachineController controller)
    {
        if (enterState == null)
            return;

        enterState.Execute(controller);
    }
}
