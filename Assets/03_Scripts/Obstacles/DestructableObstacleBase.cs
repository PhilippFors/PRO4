using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObstacleBase : MonoBehaviour
{
    protected readonly float _maxHealth;
    protected float _health;
    protected float _regenPause;
    protected float _regenRate;
    protected bool regenerate = false;

    private void Update()
    {
        if (regenerate)
        {
            _health += _regenRate * Time.deltaTime;
            if (_health >= _maxHealth)
                regenerate = false;
        }
    }

    public void ReceiveDamage(float value)
    {
        _health -= value;
        regenerate = false;
        StopCoroutine("RegenerateTimer");
        StartCoroutine("RegenerateTimer");
    }

    public float GetHealthVal()
    {
        return _health;
    }

    IEnumerator RegenerateTimer()
    {
        yield return new WaitForSeconds(_regenPause);
        regenerate = true;
    }
}
