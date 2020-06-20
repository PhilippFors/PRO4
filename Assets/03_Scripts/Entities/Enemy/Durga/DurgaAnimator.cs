using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurgaAnimator : MonoBehaviour, IEnemyActions
{

    public Animator animator => GetComponent<Animator>();
    public GameObject weapon;

    public void Attack(int i = -1)
    {

    }

    public void CancelAttack()
    {
        throw new System.NotImplementedException();
    }

    public void Walk(StateMachineController s = null)
    {
        if (s.agent.isStopped)
        {
            s.agent.isStopped = false;
        }
    }

    public void StopWalking(StateMachineController s = null)
    {
        s.agent.isStopped = true;
    }

    public void Stunned()
    {
        throw new System.NotImplementedException();
    }
}
