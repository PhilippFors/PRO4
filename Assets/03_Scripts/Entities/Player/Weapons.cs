using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public List<AttackSO> attacks = new List<AttackSO>();
    public WeaponSettings stats;
    public AttackSO baseAttack;
    public AttackStateMachine attack => GameObject.FindGameObjectWithTag("Player").GetComponent<AttackStateMachine>();

    internal void Equip(Transform weaponPoint)
    {
        transform.gameObject.SetActive(true);
        transform.SetParent(weaponPoint);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    internal void Unequip()
    {
        transform.SetParent(null);
        transform.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (attack.currentState.canDamage)
        {
            if (other.gameObject.GetComponent<EnemyBody>() != null)
            {
                GetComponentInParent<PlayerAttack>().comboCounter += 1;
                EventSystem.instance.OnAttack(other.gameObject.GetComponent<IHasHealth>(), stats.bsdmg);
                other.GetComponent<IKnockback>().ApplyKnockback(stats.knockbackForce, stats.stunChance);
            }
            else if (other.gameObject.GetComponent<ObstacleBody>())
            {
                EventSystem.instance.OnAttack(other.gameObject.GetComponent<IHasHealth>(), stats.bsdmg);
            }
        }
    }
}
