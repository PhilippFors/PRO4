using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author("Philipp Forstner")]
public class EnemyTestWeapon : MonoBehaviour
{
    public float basedmg = 5.0f;
    public bool isAttacking = false;

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;

        if (obj.GetComponent<EnemyBody>() == null && obj.GetComponent<IHasHealth>() != null)
        {
            MyEventSystem.instance.OnAttack(obj.GetComponent<IHasHealth>(), basedmg);
        }
    }

}
