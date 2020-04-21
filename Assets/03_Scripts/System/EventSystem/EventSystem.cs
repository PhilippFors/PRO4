using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{
    
    public event System.Action ResetMult;
    public event Action<EnemyBaseClass, float> AttackEnemy;
    public event Action<PlayerBody, float> AttackPlayer;
    public event Action<DestructableObstacleBase, float> AttackObstacle;
    public event Action<MultiplierName, float> ActivateSkill;


    //Events die von der Musik ausgelöst werden
    public event System.Action Kick;
    public event System.Action Bass;

    public static EventSystem _instance;
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

    public void OnAttack(EnemyBaseClass enemy, float basedmg)
    {
        AttackEnemy(enemy, basedmg);
    }

    public void OnAttack(PlayerBody player, float basedmg)
    {
        AttackPlayer(player, basedmg);
    }

    public void OnAttack(DestructableObstacleBase obstacle, float basedmg)
    {
        AttackObstacle(obstacle, basedmg);
    }

    public void OnSkill(MultiplierName multiplierName, float value)
    {
        ActivateSkill(multiplierName, value);
    }

    public void OnSkill()
    {
        ResetMult();
    }

    public void OnKick()
    {
        if(Kick == null)
        {
            Debug.Log("KickEvent has no subscriber");
        }
        else
        {
            Kick();
        }

        //WHY THIS SHORTHAND SYNTAX NICHT WORKING?
       //Kick == null ? Debug.Log("KickEvent is empty") : Kick();
    }

    public void OnBass()
    {
        if (Bass == null)
        {
            Debug.Log("BassEvent has no subscriber");
        }
        else
        {
            Bass();
        }
    }
}
