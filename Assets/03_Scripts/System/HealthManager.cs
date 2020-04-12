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
        float damage = baseDmg * MultiplierManager.instance.GetEnemyMultValue(MultiplierName.damageMod);
        damage = damage * damage / (damage + (enemy.GetStatValue(StatName.defense) * MultiplierManager.instance.GetEnemyMultValue(MultiplierName.defense)));
        //float damage = baseDmg * (baseDmg/(baseDmg + enemy.GetStat(EnemyStatName.defense)))
        enemy.SetStatValue(StatName.health, damage);
    }

    public void calcDmg(PlayerBody player, float baseDmg)
    {
        
        float damage = baseDmg * baseDmg / (baseDmg + player.GetStatValue(StatName.defense));
        //float damage = baseDmg * (baseDmg/(baseDmg + enemy.GetStat(EnemyStatName.defense)))
        player.SetStatValue(StatName.health, damage);
    }

    public void calcDmg(DestructableObstacleBase obstacle, float baseDmg)
    {
        obstacle.ReceiveDamage(baseDmg);
    }


}
