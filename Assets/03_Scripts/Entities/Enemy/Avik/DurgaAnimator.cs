using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurgaAnimator : MonoBehaviour, IEnemyActions
{

    public Animator animator => GetComponent<Animator>();
    public GameObject weapon;

    public void Attack(int i = 0)
    {
        switch (i)
        {
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
        }
    }



    public void CancelAttack(StateMachineController controller)
    {
        animator.SetTrigger("cancel");
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

    public void Stunned(StateMachineController controller)
    {
        throw new System.NotImplementedException();
    }
}
