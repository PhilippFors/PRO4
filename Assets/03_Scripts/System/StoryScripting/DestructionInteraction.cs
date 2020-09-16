using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionInteraction : MonoBehaviour, IHasHealth
{
    public float health;
    public GameObject destroyedPrefab;
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
        EventSystem.instance.GoalDestroyed();
        destroyedPrefab.SetActive(true);
        gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        CheckHealth();
    }

    
}
