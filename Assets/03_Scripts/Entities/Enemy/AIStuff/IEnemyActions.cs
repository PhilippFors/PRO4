public interface IEnemyActions
{
    void Attack(int i);
    void CancelAttack();

    void Walk(StateMachineController s);
    void StopWalking(StateMachineController s);
    void Stunned();
}
