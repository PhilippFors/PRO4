using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Grenade : MonoBehaviour
{
    private float radius = 6f;
    public float force = 700f;
    public float dmg = 15f;
    private GameObject explosionEffect;
    // Start is called before the first frame update
    void Start()
    {
        MyEventSystem.instance.Explode += Explode;
    }

    private void OnDisable()
    {
        MyEventSystem.instance.Explode -= Explode;

    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision other)
    {
        Explode();
    }

    public void Explode()
    {
        // Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Enemy"));

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
                // rb.gameObject.GetComponent<StateMachineController>().Stun();
                MyEventSystem.instance.OnAttack(nearbyObject.gameObject.GetComponent<IHasHealth>(), dmg);
            }
            else if (nearbyObject.gameObject.GetComponent<IHasHealth>() != null)
            {
                MyEventSystem.instance.OnAttack(nearbyObject.gameObject.GetComponent<IHasHealth>(), dmg);
            }
        }

        Destroy(gameObject);
    }
}
