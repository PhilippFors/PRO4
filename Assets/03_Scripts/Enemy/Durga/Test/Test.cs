﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    public SubClass ting;
    void Start()
    {
        TesteEvent.instance.OnActivation += doThing;
         
    }

    void doThing(SuperClass thing, float dmg){
        float newhealth = thing.getValue(EnemyStatName.health)-dmg;
        thing.SetStat(EnemyStatName.health, newhealth);
    }

}
