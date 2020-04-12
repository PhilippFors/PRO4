using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestWeapon : MonoBehaviour
{
    float bsdmg = 5.0f;

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.GetComponent<EnemyBaseClass>() != null)
        {
            EventSystem.instance.OnAttack(obj.GetComponent<EnemyBaseClass>(), bsdmg);
        }
        else if (obj.GetComponent<DestructableObstacleBase>() != null)
        {
            EventSystem.instance.OnAttack(obj.GetComponent<DestructableObstacleBase>(), bsdmg);
        }
    }
}
