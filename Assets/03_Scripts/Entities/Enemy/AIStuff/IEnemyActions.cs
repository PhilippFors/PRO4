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
    void Attack(StateMachineController s, int i, bool combo = false);
    void CancelAttack(StateMachineController s);
    void StopAttack(StateMachineController s);

    void Walk(StateMachineController s);
    void StopWalking(StateMachineController s);
    void Stunned(StateMachineController s);

    bool CheckIsAttacking(StateMachineController s);
}
