using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ar_damagePlateCollider : MonoBehaviour
{
    bool disabled = false;

    void OnTriggerEnter(Collider c)
    {
       
        gameObject.GetComponentInParent<AR_damagePlate>().PullTrigger(c, 30);
    }

    private void OnTriggerStay(Collider c)
    {
        gameObject.GetComponentInParent<AR_damagePlate>().PullTrigger(c, 5);
    }

    public void DisableSelf()
    {
        GetComponent<BoxCollider>().enabled = false;
        disabled = true;
    }

    public void EnableSelf()
    {
        GetComponent<BoxCollider>().enabled = true;
        disabled = false;
    }



}
