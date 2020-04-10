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
    float _defense;

    public HealthManager healthManager;
    public EnemyTemplate durgaTemplate;
    private void Start()
    {
        _health = durgaTemplate.health;
        _baseDmg = durgaTemplate.baseDmg;
        _speed = durgaTemplate.speed;
        _range = durgaTemplate.range;
        _turnSpeed = durgaTemplate.turnSpeed;
        _defense = durgaTemplate.defense;
    }

    void OnDeath(){
        Debug.Log("I died!");
    }

    void CheckHealth(){
        if(_health <= 0){
            OnDeath();
            Destroy(gameObject);
        }
    }

    public void setHealth(float dmg)
    {
        _health -= dmg;
        CheckHealth();
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
    public float getDefense(){
        return _defense;
    }
}
