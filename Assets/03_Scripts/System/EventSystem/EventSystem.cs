using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{
    public event Action<IHasHealth, float> Attack;
    public event Action<Skills> ActivateSkill;

    public event Action<PlayerMovementSate> SetState;

    //Events die von der Musik ausgelöst werden
    public event System.Action Snare;
    public event System.Action Kick;
    public event System.Action HighHat;
    public event System.Action AimGrenade;
    public event System.Action ThrowGrenade;
    public event System.Action Explode;
    

    //Events for Enemy managment
    public event Action<EnemyBody> onEnemyDeath;
    public event Action<EnemyBody> activateAI;
    public static EventSystem instance;

    private void Awake()
    {
        instance = this;
    }

    public void OnAttack(IHasHealth entity, float basedmg)
    {
        if (Attack != null)
            Attack(entity, basedmg);
    }

    public void OnSkill(Skills skill)
    {
        if (ActivateSkill != null)
            Debug.Log("Skill Activated");
        ActivateSkill(skill);
    }

    public void OnSnare()
    {
        if (Snare == null)
        {
            Debug.Log("SnareEvent has no subscriber");
        }
        else
        {
            Snare();
        }

        //WHY THIS SHORTHAND SYNTAX NICHT WORKING?
        //Kick == null ? Debug.Log("KickEvent is empty") : Kick();
        //No idea but du not brauchen it -P
    }

    public void OnHighHat()
    {
        if (HighHat == null)
        {
            Debug.Log("HighHatEvent has no subscriber");
        }
        else
        {
            HighHat();
        }


    }

    public void OnKick()
    {
        if (Kick == null)
        {
            Debug.Log("KickEvent has no subscriber");
        }
        else
        {
            Kick();
        }
    }

    public void OnEnemyDeath(EnemyBody enemy)
    {
        if (onEnemyDeath != null)
            onEnemyDeath(enemy);
    }

    public void ActivateAI(EnemyBody enemy)
    {
        if (activateAI != null)
            activateAI(enemy);
    }

    public void OnGrenadeAim()
    {
        AimGrenade();
    }

    public void onGrenadeRelease()
    {
    }

    public void OnExplode()
    {
        Explode();
    }

    public void OnSetState(PlayerMovementSate state)
    {
        SetState(state);
    }
    
}