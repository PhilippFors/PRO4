using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Grenade : MonoBehaviour
{
    private float radius = 10f;
    public float force = 700f;

    private GameObject explosionEffect;
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.instance.Explode += Explode;
    }

    private void OnDisable()
    {
        EventSystem.instance.Explode -= Explode;

    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
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
                rb.gameObject.GetComponent<StateMachineController>().Stun();
            }
        }

        Destroy(gameObject);
    }
}
