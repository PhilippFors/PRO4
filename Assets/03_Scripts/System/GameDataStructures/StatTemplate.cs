using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author(mainAuthor = "Philipp Forstner")]
[CreateAssetMenu(fileName = "New StatTemplate", menuName = "StatTemplate")]
public class StatTemplate : ScriptableObject
{
    public List<FloatReference> statList = new List<FloatReference>();
    // public float health;
    // public float baseDmg;
    // public float dmgModifier;
    // public float speed;
    // public float range;
    // public float turnSpeed;
    // public float defense;

    // public float attackSpeed;

    public void Add(FloatVariable s){
        FloatReference f = new FloatReference();
        f.Variable = s;
        statList.Add(f);
    }
}
