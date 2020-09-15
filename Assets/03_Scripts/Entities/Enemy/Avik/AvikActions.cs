using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvikActions : MonoBehaviour, IEnemyActions
{
    public Animator animator;
    public Weapon[] weapons;
    float extraComboTime = 1f;
    public ParticleSystem stunParticle;

    public void Attack(StateMachineController controller, int i, bool combo = false)
    {
        switch (i)
        {
            case 1:
                ExecuteAttack(controller, i, combo);
                break;
            case -1:
                animator.Play("Activation");
                break;
        }
        if (combo)
        {
            StartCoroutine(AttackDelay(controller, extraComboTime));
        }
        else
        {
            StartCoroutine(AttackDelay(controller));
        }

    }

    void ExecuteAttack(StateMachineController controller, int i, bool combo)
    {
        foreach (Weapon weapon in weapons)
            weapon.Activate();

        controller.canAttack = false;
        controller.isAttacking = true;
        animator.SetFloat(AnimatorStrings.animSpeed.ToString(), controller.enemyStats.GetStatValue(StatName.AttackSpeed));
        animator.SetInteger(AnimatorStrings.attacknr.ToString(), i);
        if (combo)
            animator.SetTrigger(AnimatorStrings.comboTrigger.ToString());

    }

    private void SingleAttack()
    {
        animator.SetInteger(AnimatorStrings.attacknr.ToString(), 1);
    }

    public void CancelAttack(StateMachineController controller)
    {
        animator.SetTrigger(AnimatorStrings.cancel.ToString());
    }
    public void StopAttack(StateMachineController controller)
    {
        animator.SetTrigger(AnimatorStrings.stop.ToString());
    }
    public void Walk(StateMachineController s)
    {
        if (s.agent.isStopped)
        {
            s.agent.isStopped = false;
        }
        animator.SetBool("isWalking", true);
    }

    public void StopWalking(StateMachineController s)
    {
        s.agent.isStopped = true;
        animator.SetBool("isWalking", false);
    }

    public void Stunned(StateMachineController controller)
    {
        stunParticle.Play();
    }

    public bool CheckIsAttacking(StateMachineController controller)
    {
        if (animator.GetInteger(AnimatorStrings.attacknr.ToString()) == 0)
        {
            controller.isAttacking = false;
            foreach (Weapon weapon in weapons)
                weapon.Deactivate();


        }
        return controller.isAttacking;
    }

    public float GetAttackCountdown()
    {
        return 0;
    }

    IEnumerator AttackDelay(StateMachineController controller, float extra = 0)
    {
        yield return new WaitForSeconds(controller.enemyStats.GetStatValue(StatName.AttackRate) + extra);

        controller.canAttack = true;
    }

    public Animator GetAnimator()
    {
        return null;
    }

    public void Init()
    {
        
    }
}
