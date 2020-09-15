using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionInteraction : MonoBehaviour, IHasHealth
{
    public float health;

    public void Heal(float healAmount)
    {

    }

    void CheckHealth()
    {
        if (health <= 0)
            OnDeath();
    }
    public void OnDeath()
    {
        StoryEventSystem.instance.Progress();
        enabled = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        CheckHealth();
    }

    
}
