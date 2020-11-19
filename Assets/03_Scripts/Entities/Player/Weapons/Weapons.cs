using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public List<AttackSO> attacks = new List<AttackSO>();
    public WeaponSettings stats;
    public AttackSO baseAttack;
    public AttackStateMachine attackSt;
    public PlayerAttack playerAttack;
    Animator animator => attackSt.animator;
    internal void Equip(Transform weaponPoint)
    {
        switch (stats.weaponName)
        {
            case WeaponNames.Hammer:
                animator.SetTrigger("toHammer");

                break;
            case WeaponNames.Dagger:
                animator.SetTrigger("toDagger");
                break;
        }
        attackSt.SetBase(baseAttack);
        transform.gameObject.SetActive(true);
        transform.SetParent(weaponPoint);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    internal void Unequip()
    {
        transform.SetParent(null);
        transform.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (attackSt.currentState.canDamage)
        {
            if (other.gameObject.GetComponent<EnemyBody>() != null)
            {
                GetComponentInParent<PlayerAttack>().comboCounter += 1;
                float damage = stats.bsdmg * (1 + attackSt.currentAttack.comboDamageMultiplier * (playerAttack.comboCounter - 1));

                MyEventSystem.instance.OnAttack(other.gameObject.GetComponent<IHasHealth>(), damage);

                // other.GetComponent<IKnockback>().ApplyKnockback(stats.knockbackForce);
                // other.GetComponent<IKnockback>().ApplyStun(stats.stunChance);
            }
            else if (other.gameObject.GetComponent<ObstacleBody>())
            {
                MyEventSystem.instance.OnAttack(other.gameObject.GetComponent<IHasHealth>(), stats.bsdmg);
            }
        }
    }
}
