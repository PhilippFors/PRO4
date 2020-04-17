using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DurgaBody : EnemyBaseClass
{

    [SerializeField] private EnemyTemplate durgaTemplate;
    
    private void Start()
    {
        statList = StatInit.InitEnemyStats(durgaTemplate);
        multList = StatInit.InitEnemyMultipliers();
    }
}
