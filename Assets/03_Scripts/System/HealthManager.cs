using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    private void Start()
    {
        EventSystem.instance.AttackPlayer += calcDmg;
        EventSystem.instance.AttackEnemy += calcDmg;
        EventSystem.instance.AttackObstacle += calcDmg;
    }

    public void calcDmg(EnemyBaseClass enemy, float baseDmg)
    {
        float damage = baseDmg * MultiplierManager.instance.GetMultiplierValue(MultiplierName.damageMod);
        damage = damage * damage / (damage + (enemy.GetStat(EnemyStatName.defense) * MultiplierManager.instance.GetMultiplierValue(MultiplierName.defense)));
        //float damage = baseDmg * (baseDmg/(baseDmg + enemy.GetStat(EnemyStatName.defense)))
        enemy.SetStat(EnemyStatName.health, enemy.GetStat(EnemyStatName.health) - damage);
    }

    public void calcDmg(PlayerBody player, float baseDmg)
    {
        player.setHealth(1f);
    }

    public void calcDmg(ObstacleBaseClass obstacle, float baseDmg)
    {
        // obstacle.setHealth(1f);
    }

    private void OnDisable()
    {
        EventSystem.instance.AttackPlayer -= calcDmg;
        EventSystem.instance.AttackEnemy -= calcDmg;
        EventSystem.instance.AttackObstacle -= calcDmg;
    }
}
