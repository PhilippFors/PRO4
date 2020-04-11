using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{

    public event Action<IEnemyBase, float> AttackEnemy;
    public event Action<PlayerBody, float> AttackPlayer;
    public event Action<IObstacleBase, float> AttackObstacle;

    private static EventSystem _instance;
    public static EventSystem instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("null");

            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    public void OnAttack(IEnemyBase enemy, float basedmg)
    {
        AttackEnemy(enemy, basedmg);
    }

    public void OnAttack(PlayerBody player, float basedmg)
    {
        AttackPlayer(player, basedmg);
    }

    public void OnAttack(IObstacleBase obstacle, float basedmg)
    {
        AttackObstacle(obstacle, basedmg);
    }


}
