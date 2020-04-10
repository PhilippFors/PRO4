using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestWeapon : MonoBehaviour
{
    float bsdmg = 5.0f;

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.GetComponent<IEnemyBase>() != null)
        {
            EventSystem.instance.OnAttack(obj.GetComponent<IEnemyBase>(), bsdmg);
        }
        else if (obj.GetComponent<IObstacleBase>() != null)
        {
            EventSystem.instance.OnAttack(obj.GetComponent<IObstacleBase>(), bsdmg);
        }
    }
}
