using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvikActions : MonoBehaviour, IEnemyActions
{
    public Animator animator;
    public GameObject weapon;
    float attackCountdown;
    public void Attack(StateMachineController controller, int i, int j = 0)
    {
        switch (i)
        {
            case 1:
                ExecuteAttack(controller, i, j);
                break;
            case 2:
                ExecuteAttack(controller, i, j);
                break;
            case -1:
                animator.Play("Activation");
                break;
        }
        if(j != 0){
            StartCoroutine(AttackDelay(controller, 1f));
        } else{
            StartCoroutine(AttackDelay(controller));
        }
                
    }

    void ExecuteAttack(StateMachineController controller, int i, int j = 0)
    {
        controller.canAttack = false;
        controller.isAttacking = true;
        animator.SetFloat("AnimSpeed", controller.enemyStats.GetStatValue(StatName.AttackSpeed));
        animator.SetInteger("attacknr", i);
        animator.SetInteger("secAttacknr", j);
        

    }

    void CheckIsAttacking(StateMachineController controller)
    {
        if (animator.GetInteger("attacknr") == 0)
        {
            controller.isAttacking = false;
        }
    }
    private void SingleAttack()
    {
        animator.SetInteger("attacknr", 1);
    }

    public void CancelAttack(StateMachineController controller)
    {
        animator.SetTrigger("cancel");
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

    IEnumerator AttackDelay(StateMachineController controller, float extra = 0)
    {
        yield return new WaitForSeconds(controller.enemyStats.GetStatValue(StatName.AttackRate)+extra);

        controller.canAttack = true;
    }
}
