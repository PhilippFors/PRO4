using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvikActions : MonoBehaviour, IEnemyActions
{
    public Animator animator;
    public Weapon[] weapons;
    float attackCountdown;
    float extraComboTime = 1f;

    public void Attack(StateMachineController controller, int i, bool combo = false)
    {
        switch (i)
        {
            case 1:
                ExecuteAttack(controller, i, combo);
                break;
            case 2:
                ExecuteAttack(controller, i, combo);
                break;
            case -1:
                animator.Play("Activation");
                break;
        }
        if (combo)
        {
            StartCoroutine(controller.settings.AttackDelay(controller, extraComboTime));
        }
        else
        {
            StartCoroutine(controller.settings.AttackDelay(controller));
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

    public void Walk(StateMachineController s)
    {
        if (s.agent.isStopped)
        {
            s.agent.isStopped = false;
        }
        animator.SetTrigger("isWalking");
    }

    public void StopWalking(StateMachineController s)
    {
        s.agent.isStopped = true;
        animator.ResetTrigger("isWalking");
    }

    public void Stunned(StateMachineController controller)
    {
        throw new System.NotImplementedException();
    }

    public void CheckIsAttacking(StateMachineController controller)
    {
        if (animator.GetInteger(AnimatorStrings.attacknr.ToString()) == 0)
        {
            controller.isAttacking = false;
            foreach (Weapon weapon in weapons)
                weapon.Deactivate();
        }
    }
}
