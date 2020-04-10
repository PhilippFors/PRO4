using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    float _health;
    float _speed;
    
    HealthManager healthManager;
    public void setHealth(float dmg){
        _health -=dmg;
    }

    public float getHealth(){
        return _health;
    }

    public float getSpeed(){
        return _speed;
    
    }
    
}
