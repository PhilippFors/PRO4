using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float damage;
    public Rigidbody rb => GetComponent<Rigidbody>();

    public void InitBUllet(Vector3 dir, float force, float dmg)
    {
        damage = dmg;
        rb.AddForce(dir * force, ForceMode.Impulse);
        Destroy(this.gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerBody p = other.GetComponent<PlayerBody>();
        if(p != null)
        {
            MyEventSystem.instance.OnAttack(p, damage);
        }
        Destroy(this.gameObject);
    }


}
