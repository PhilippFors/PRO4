using UnityEngine;
[Author("Philipp Forstner")]
public abstract class IEnemyAttacks : MonoBehaviour
{
    public Enemy.AttackAnimations[] attackAnimations;
    public EnemyBody enemyBody => GetComponent<EnemyBody>();
    public EnemyActions actions => GetComponent<EnemyActions>();
    public EnemyStatistics stats => GetComponent<EnemyStatistics>();

    public abstract void Attack();
    public abstract void CancelAttack();
    public abstract void StopAttack();
}
