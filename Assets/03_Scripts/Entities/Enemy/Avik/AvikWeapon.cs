using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvikWeapon : Weapon
{
    public StateMachineController controller;
    BoxCollider col => GetComponent<BoxCollider>();

    // private void Update()
    // {
    //     switch (controller.isAttacking)
    //     {
    //         case true:
    //             col.enabled = true;
    //             break;
    //         case false:
    //             col.enabled = false;
    //             break;
    //     }
    // }

    public override void Activate()
    {
        col.enabled = true;
    }

    public override void Deactivate()
    {
        col.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (controller.isAttacking)
        {
            if (!obj.GetComponent<EnemyBody>() & obj.GetComponent<IHasHealth>() != null)
            {
                MyEventSystem.instance.OnAttack(obj.GetComponent<IHasHealth>(), controller.enemyStats.GetStatValue(StatName.BaseDmg) * controller.enemyStats.GetMultValue(MultiplierName.damage));
            }
        }
    }
}
