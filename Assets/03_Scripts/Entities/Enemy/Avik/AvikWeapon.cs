using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvikWeapon : MonoBehaviour
{
    public StateMachineController controller;
    BoxCollider col => GetComponent<BoxCollider>();

    private void Update()
    {
        switch (controller.isAttacking)
        {
            case true:
                col.enabled = true;
                break;
            case false:
                col.enabled = false;
                break;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (controller.isAttacking)
        {
            if (!obj.GetComponent<EnemyBody>() & obj.GetComponent<IHasHealth>() != null)
            {
                EventSystem.instance.OnAttack(obj.GetComponent<IHasHealth>(), controller.enemyStats.GetStatValue(StatName.BaseDmg));
            }
        }
    }
}
