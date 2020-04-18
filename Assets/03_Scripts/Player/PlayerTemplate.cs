using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class PlayerTemplate : ScriptableObject
{
    public float health;
    public float speed;
    public float attackSpeed;
    public float defense;
}
