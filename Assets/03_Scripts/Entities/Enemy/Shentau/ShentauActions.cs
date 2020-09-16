using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShentauActions : MonoBehaviour, IEnemyActions
{
    public float countdown;
    public bool canAttack = false;
    public Animator animator;
    public GameObject bullet;
    public Transform bulletPoint;
    public void Init()
    {
        StartCoroutine(Recharge());
    }
    public void Attack(StateMachineController s, int i = -1, bool combo = false)
    {
        Debug.Log("Shentau ATTACK");
        animator.SetTrigger("attack");
        Bullet b = Instantiate(bullet, bulletPoint.position, bulletPoint.rotation).GetComponent<Bullet>();
        b.InitBUllet(bulletPoint.forward, 17f, s.enemyStats.GetStatValue(StatName.BaseDmg));
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
        canAttack = false;
        if (s == null)
            countdown = 6f;
        else
            countdown = s.enemyStats.GetStatValue(StatName.AttackRate);

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
