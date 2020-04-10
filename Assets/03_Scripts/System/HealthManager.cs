using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public ModifierManager modManager;
    private void Start()
    {
        EventSystem.instance.AttackPlayer += calcDmg;
        EventSystem.instance.AttackEnemy += calcDmg;
        EventSystem.instance.AttackObstacle += calcDmg;
    }

    public void calcDmg(IEnemyBase enemy, float baseDmg)
    {
        enemy.setHealth(1f);
    }

    public void calcDmg(PlayerBody player, float baseDmg)
    {
        player.setHealth(1f);
    }

    public void calcDmg(IObstacleBase obstacle, float baseDmg)
    {
        obstacle.setHealth(1f);
    }
    
    private void OnDisable()
    {
        EventSystem.instance.AttackPlayer -= calcDmg;
        EventSystem.instance.AttackEnemy -= calcDmg;
        EventSystem.instance.AttackObstacle -= calcDmg;
    }
}
