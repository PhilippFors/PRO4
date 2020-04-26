using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestWeapon : MonoBehaviour
{
    public float bsdmg = 20.0f;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.GetComponent<EnemyBody>() != null)
        {
            GetComponentInParent<PlayerAttack>().comboCounter += 1;
            EventSystem.instance.OnAttack(other.gameObject.GetComponent<IHasHealth>(), bsdmg);
        }
        else if (other.gameObject.GetComponent<ObstacleBody>())
        {
            EventSystem.instance.OnAttack(other.gameObject.GetComponent<IHasHealth>(), bsdmg);
        }
    }
}
