using System.Collections;
using UnityEngine;

public class ObstacleBody : MonoBehaviour, IHasHealth
{
    public readonly float maxHealth;
    public float health;
    public float regenPause;
    public float regenRate;
    public bool regenerate = false;

    private void Update()
    {
        if (!regenerate)
            return;

        Heal(regenRate);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        regenerate = false;
        StopCoroutine("RegenerateTimer");
        StartCoroutine("RegenerateTimer");
    }

    public void OnDeath()
    {
        //Lower Obstacle
    }

    IEnumerator RegenerateTimer()
    {
        yield return new WaitForSeconds(regenPause);
        regenerate = true;
    }

    public void Heal(float healAmount)
    {
        health += healAmount * Time.deltaTime;
        if (health >= maxHealth)
            regenerate = false;
    }
}
