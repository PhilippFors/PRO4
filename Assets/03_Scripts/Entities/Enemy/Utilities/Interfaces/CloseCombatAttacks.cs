using System.Collections;
using UnityEngine;
[Author("Philipp Forstner")]
public abstract class CloseCombatAttacks : IEnemyAttacks
{
    protected Coroutine damageWaiter;
    protected Coroutine attackTimer;
    public override abstract void Attack();
    public override abstract void CancelAttack();
    public override abstract void StopAttack();

    public virtual bool DoDamage()
    {
        return false;
    }

    //Times the damage window with the provided frames from the attack animation
    public virtual IEnumerator AttackTimer(float startDamageFrame, float stopDamageFrame, float clipLength = 0)
    {
        float start = startDamageFrame / 24;
        float end = (stopDamageFrame - startDamageFrame) / 24;

        yield return new WaitForSeconds(start);
        actions.canDamage = true;

        yield return StartCoroutine(DamageTimer(end));

        actions.canDamage = false;

        if (clipLength != 0)
            yield return new WaitForSeconds(clipLength - end);

        actions.isAttacking = false;
    }

    protected virtual IEnumerator DamageTimer(float wait)
    {
        float cTime = 0;

        while (cTime <= wait)
        {
            if (DoDamage())
                yield break;

            cTime += Time.deltaTime;
            yield return null;
        }
    }
}
