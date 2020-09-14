using UnityEngine;
public enum AnimatorStrings
{
    undefined,

    animSpeed,

    comboTrigger,

    attacknr,

    cancel,

    stop
}

public interface IEnemyActions
{
    void Init();
    void Attack(StateMachineController s, int i = -1, bool combo = false);
    void CancelAttack(StateMachineController s);
    void StopAttack(StateMachineController s);

    void Walk(StateMachineController s);
    void StopWalking(StateMachineController s);
    void Stunned(StateMachineController s);

    bool CheckIsAttacking(StateMachineController s);

    float GetAttackCountdown();

    Animator GetAnimator();
}
