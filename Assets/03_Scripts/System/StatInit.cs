using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatInit : MonoBehaviour
{
    public static List<EnemyStatistics> InitEnemyStats(EnemyTemplate template){
        List<EnemyStatistics> list = new List<EnemyStatistics>();
        list.Add(new EnemyStatistics(template.health, EnemyStatName.health));
        list.Add(new EnemyStatistics(template.speed,EnemyStatName.speed));
        list.Add(new EnemyStatistics(template.turnSpeed, EnemyStatName.turnSpeed));
        list.Add(new EnemyStatistics(template.defense, EnemyStatName.defense));
        list.Add(new EnemyStatistics(template.range, EnemyStatName.range));
        return list;
    }

    public static List<Multiplier> InitModifiers(){
        List<Multiplier> list = new List<Multiplier>();
        list.Add(new Multiplier(1.0f, 1.0f, MultiplierName.speedMod));
        list.Add(new Multiplier(1.0f, 1.0f, MultiplierName.defense));

        return list;
    }
}
