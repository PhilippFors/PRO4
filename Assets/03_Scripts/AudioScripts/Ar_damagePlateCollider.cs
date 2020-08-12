using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ar_damagePlateCollider : MonoBehaviour
{
    void OnTriggerStay(Collider c)
    {
       
        gameObject.GetComponentInParent<AR_damagePlate>().PullTrigger(c);
    }
  
}
