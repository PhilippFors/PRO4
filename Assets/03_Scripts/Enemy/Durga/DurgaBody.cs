using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DurgaBody : MonoBehaviour, IEnemyBase
{
    float _health;
    float _baseDmg;
    float _speed;
    float _range;
    float _turnSpeed;

    public HealthManager healthManager;
    public EnemyTemplate durgaTemplate;
    private void Start()
    {
        _health = durgaTemplate.health;
        _baseDmg = durgaTemplate.baseDmg;
        _speed = durgaTemplate.speed;
        _range = durgaTemplate.range;
        _turnSpeed = durgaTemplate.turnSpeed;
    }

    public void setHealth(float baseDmg)
    {
        _health = healthManager.calcDmg(baseDmg, _health);
    }

    public float getHealth()
    {
        return _health;
    }

    public float getSpeed()
    {
        return _speed;
    }

    public float getRange()
    {
        return _range;
    }

    public float getBaseDmg()
    {
        return _baseDmg;
    }

    public float getTurnSpeed()
    {
        return _turnSpeed;
    }
}
