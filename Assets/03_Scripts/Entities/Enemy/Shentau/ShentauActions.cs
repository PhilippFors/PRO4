using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShentauActions : MonoBehaviour, IEnemyActions
{
    public float countdown;
    bool canAttack = false;
    public Animator animator;
    private void Start()
    {
        StartCoroutine(Recharge());
    }
    public void Attack(StateMachineController s, int i = -1, bool combo = false)
    {
        canAttack = false;

        Debug.Log("Shentau ATTACK");
        animator.SetTrigger("attack");

        StartCoroutine(Recharge(s));
    }

    public void CancelAttack(StateMachineController s)
    {
        throw new System.NotImplementedException();
    }

    public bool CheckIsAttacking(StateMachineController s)
    {
        return canAttack;
    }

    public float GetAttackCountdown()
    {
        return countdown;
    }

    public void StopAttack(StateMachineController s)
    {

    }

    public void StopWalking(StateMachineController s)
    {

    }

    public void Stunned(StateMachineController s)
    {

    }

    public void Walk(StateMachineController s)
    {

    }

    IEnumerator Recharge(StateMachineController s = null)
    {
        yield return null;
        if (s != null)
            countdown = s.enemyStats.GetStatValue(StatName.AttackRate);
        else
            countdown = 6f;

        while (countdown >= 0)
        {
            yield return new WaitForSeconds(0.1f);
            countdown -= 0.1f;
        }

        canAttack = true;
    }

    public Animator GetAnimator()
    {
        return animator;
    }
}
