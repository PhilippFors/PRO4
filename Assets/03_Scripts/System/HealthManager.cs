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
    private void OnDisable()
    {
        EventSystem.instance.AttackPlayer -= calcDmg;
        EventSystem.instance.AttackEnemy -= calcDmg;
        EventSystem.instance.AttackObstacle -= calcDmg;
    }
    
    public void calcDmg(EnemyBaseClass enemy, float baseDmg)
    {
        float initHealth = enemy.GetStatValue(StatName.health);
        float damage = baseDmg * enemy.GetMultValue(MultiplierName.damage);
        // damage = damage * damage / (damage + (enemy.GetStatValue(StatName.defense) * MultiplierManager.instance.GetEnemyMultValue(MultiplierName.defense)));
        damage = baseDmg * (baseDmg / (baseDmg + (enemy.GetStatValue(StatName.defense) * enemy.GetMultValue(MultiplierName.defense))));

        enemy.SetStatValue(StatName.health, initHealth - damage);
    }

    public void calcDmg(PlayerBody player, float baseDmg)
    {
        float initHealth = player.GetStatValue(StatName.health);
        float damage = baseDmg * baseDmg / (baseDmg + player.GetStatValue(StatName.defense));
        //float damage = baseDmg * (baseDmg/(baseDmg + enemy.GetStat(EnemyStatName.defense)))
        player.SetStatValue(StatName.health, initHealth - damage);
    }

    public void calcDmg(DestructableObstacleBase obstacle, float baseDmg)
    {
        obstacle.ReceiveDamage(baseDmg);
    }
}
