using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    private Body enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //generall method for damaging effects
    public void OnTriggerEnter(Collider other)
    {
        enemy = other.GetComponent<Body>(); //der body vom getroffenen gegner
        if (other.gameObject.CompareTag("Enemy")) //compares to enemies
        {
            enemy.SetHealth(gameObject.GetComponentInParent<Body>().damage); //verringert leben des gegners um den damage von spieler
        }
    }
}
