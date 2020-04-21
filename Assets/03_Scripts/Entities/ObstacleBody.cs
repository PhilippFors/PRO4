using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBody : MonoBehaviour, IHasHealth
{
    protected readonly float _maxHealth;
    protected float _health;
    protected float _regenPause;
    protected float _regenRate;
    protected bool regenerate = false;

    private void Update()
    {
        if (!regenerate)
            return;

        _health += _regenRate * Time.deltaTime;
        if (_health >= _maxHealth)
            regenerate = false;
    }
    public void CalculateHealth(float damage)
    {
        _health -= damage;
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
        yield return new WaitForSeconds(_regenPause);
        regenerate = true;
    }

}
