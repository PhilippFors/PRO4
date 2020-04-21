using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestWeapon : MonoBehaviour
{
    public float bsdmg = 20.0f;

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.GetComponent<EnemyBody>() != null)
        {
            GetComponentInParent<PlayerAttack>().comboCounter += 1;
            EventSystem.instance.OnAttack(obj.GetComponent<IHasHealth>(), bsdmg);
        }
        else if (obj.GetComponent<ObstacleBody>())
        {
            EventSystem.instance.OnAttack(obj.GetComponent<IHasHealth>(), bsdmg);
        }
    }
}
