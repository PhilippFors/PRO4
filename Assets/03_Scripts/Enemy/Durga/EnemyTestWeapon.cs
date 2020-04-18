using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestWeapon : MonoBehaviour
{
    public float basedmg = 5.0f;

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.GetComponent<PlayerBody>() != null)
        {
            EventSystem.instance.OnAttack(obj.GetComponent<PlayerBody>(), basedmg);
        }
        else if (obj.GetComponent<DestructableObstacleBase>() != null)
        {
            EventSystem.instance.OnAttack(obj.GetComponent<DestructableObstacleBase>(), basedmg);
        }
    }
}
