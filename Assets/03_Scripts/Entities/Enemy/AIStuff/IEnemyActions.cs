public interface IEnemyActions
{
    void Attack(int i);
    void CancelAttack(StateMachineController s);

    void Walk(StateMachineController s);
    void StopWalking(StateMachineController s);
    void Stunned(StateMachineController s);
}
