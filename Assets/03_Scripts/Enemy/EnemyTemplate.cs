using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyTemplate : ScriptableObject
{
    public float health;
    public float baseDmg;
    public float dmgModifier;
    public float speed;
    public float range;
    public float turnSpeed;

}
