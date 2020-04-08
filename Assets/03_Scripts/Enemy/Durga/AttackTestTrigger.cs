using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTestTrigger : MonoBehaviour
{
    float bsdmg = 5.0f;
    public bool trig = false;
    public  HealthManager healthManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IEnemyBase>() != null)
        {
            other.gameObject.GetComponent<IEnemyBase>().setHealth(bsdmg);
        }
    }
}
