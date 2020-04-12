using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubClass : SuperClass
{

    public EnemyTemplate template;
    void Start()
    {
        Debug.Log("I started.");
        list = StatInit.InitEnemyStats(template);
    }

    protected override void OnDeath()
    {
        Debug.Log("I died in the Subclass!" + gameObject.name);
    }

   
}
