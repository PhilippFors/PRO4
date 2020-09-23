using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_beaconWallCollider : MonoBehaviour
{
    bool disabled = false;

    float dmgOnEnter;
    float dmgOnStay;

    public void Start()
    {
        dmgOnEnter = gameObject.GetComponentInParent<AR_beaconWall>().dmgOnEnter;
        dmgOnStay = gameObject.GetComponentInParent<AR_beaconWall>().dmgOnStay;

    }
    void OnTriggerEnter(Collider c)
    {

        gameObject.GetComponentInParent<AR_beaconWall>().PullTrigger(c, dmgOnEnter);
    }

    private void OnTriggerStay(Collider c)
    {
        gameObject.GetComponentInParent<AR_beaconWall>().PullTrigger(c, dmgOnStay);
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
