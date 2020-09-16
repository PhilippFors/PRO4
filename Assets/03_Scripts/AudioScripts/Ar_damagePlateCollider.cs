using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ar_damagePlateCollider : MonoBehaviour
{
    bool disabled = false;

    float dmgOnEnter;
    float dmgOnStay;

    public void Start()
    {
        dmgOnEnter = gameObject.GetComponentInParent<AR_damagePlate>().dmgOnEnter;
        dmgOnStay = gameObject.GetComponentInParent<AR_damagePlate>().dmgOnStay;

    }
    void OnTriggerEnter(Collider c)
    {
       
        gameObject.GetComponentInParent<AR_damagePlate>().PullTrigger(c, dmgOnEnter);
    }

    private void OnTriggerStay(Collider c)
    {
        gameObject.GetComponentInParent<AR_damagePlate>().PullTrigger(c, dmgOnStay);
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
