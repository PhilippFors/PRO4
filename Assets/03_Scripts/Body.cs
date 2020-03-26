using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public int maxHealth;

    public int health;

    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    //verringert leben eines characters um den damage eines anderen (wird meistens wo anders aufgerufen)
    public void SetHealth(int damage)
    {
        
        health = health - damage;
    }
}
