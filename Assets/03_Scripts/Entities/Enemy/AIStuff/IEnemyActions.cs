public interface IEnemyActions
{
    void Attack(StateMachineController s, int i, bool combo = false);
    void CancelAttack(StateMachineController s);

    void Walk(StateMachineController s);
    void StopWalking(StateMachineController s);
    void Stunned(StateMachineController s);
}
