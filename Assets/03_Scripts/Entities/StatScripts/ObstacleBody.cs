using System.Collections;
using UnityEngine;

public class ObstacleBody : MonoBehaviour, IHasHealth
{
    
    public float health;
    public float minActivationHealth = 20f;
    public float regenPause = 2f;
    public float regenRate = 10f;
    public bool regenActive = false;
    public FloatVariable maxHealth;
    BoxCollider col => GetComponent<BoxCollider>();
    MeshRenderer render => GetComponent<MeshRenderer>();
    private void Start()
    {
        health = maxHealth.Value;
    }
    private void Update()
    {
        if (!regenActive)
            return;

        Heal(regenRate);

    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
            health = 0;
        regenActive = false;
        StopCoroutine("RegenerateTimer");
        StartCoroutine("RegenerateTimer");
    }

    public void OnDeath()
    {
        col.enabled = false;
        render.enabled = false;
        //TODO: Disable Obstacle, animate deactivation
    }

    public void Activate(){
        col.enabled = true;
        render.enabled = true;
        //TODO: Enable Obstalce, animate activation
    }

    IEnumerator RegenerateTimer()
    {
        yield return new WaitForSeconds(regenPause);
        regenActive = true;
    }

    public void Heal(float healAmount)
    {
        health += healAmount * Time.deltaTime;
        if (health >= maxHealth.Value){
            health = maxHealth.Value;
            regenActive = false;
        }
    }
}
