using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatInit : MonoBehaviour
{
    public static List<GameStatistics> InitEnemyStats(EnemyTemplate template)
    {
        List<GameStatistics> list = new List<GameStatistics>();
        list.Add(new GameStatistics(template.health, StatName.health));
        list.Add(new GameStatistics(template.speed, StatName.speed));
        list.Add(new GameStatistics(template.turnSpeed, StatName.turnSpeed));
        list.Add(new GameStatistics(template.defense, StatName.defense));
        list.Add(new GameStatistics(template.range, StatName.range));
        return list;
    }
   
    public static List<GameStatistics> InitPlayerStats(PlayerTemplate template)
    {
        List<GameStatistics> list = new List<GameStatistics>();
        list.Add(new GameStatistics(template.health, StatName.health));
        list.Add(new GameStatistics(template.speed, StatName.speed));
        list.Add(new GameStatistics(template.attackSpeed, StatName.attackSpeed));
        list.Add(new GameStatistics(template.defense, StatName.defense));
        return list;

    }

    public static List<Multiplier> InitEnemyMultipliers()
    {
        List<Multiplier> list = new List<Multiplier>();
        list.Add(new Multiplier(1.0f, 1.0f, MultiplierName.speed));
        list.Add(new Multiplier(1.0f, 1.0f, MultiplierName.defense));
        list.Add(new Multiplier(1.0f, 1.0f, MultiplierName.health));
        list.Add(new Multiplier(1.0f, 1.0f, MultiplierName.damage));
        return list;
    }
     public static List<Multiplier> InitPlayerMultipliers(){
        List<Multiplier> list = new List<Multiplier>();
        list.Add(new Multiplier(1.0f, 1.0f, MultiplierName.defense));
        return list;
    }
}
