using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyEventSystem : MonoBehaviour
{
    public event Action<IHasHealth, float> Attack;
    public event Action<Skills> ActivateSkill;
    public event Action<Skills> DeactivateSkill;

    public event Action<PlayerMovementSate> SetState;

    //Events die von der Musik ausgelöst werden
    public event System.Action Snare;
    public event System.Action Kick;
    public event System.Action HighHat;
    public event System.Action Deactivate;

    public event System.Action AimGrenade;
    public event System.Action ThrowGrenade;
    public event System.Action Explode;


    //Events for Enemy managment
    public event Action<EnemyBody> onEnemyDeath;
    public event Action<EnemyBody> activateAI;
    public static MyEventSystem instance;

    public event System.Action goalDestroyed;
    public event System.Action waveDefeated;

    public event Action<Transform, Transform, Transform, Transform> startCamAnim;
    public event Action<Transform, Transform> notifyCamManager;

    private void Awake()
    {
        instance = this;
    }

    public void WaveDefeated()
    {
        if (waveDefeated != null)
            waveDefeated();
    }
    public void StartCamAnim(Transform camera, Transform endposition, Transform player, Transform playerDest)
    {
        if (startCamAnim != null)
            startCamAnim(camera, endposition, player, playerDest);
    }
    public void NotifyCamManager(Transform endposition, Transform playerDest)
    {
        if (notifyCamManager != null)
            notifyCamManager(endposition, playerDest);
    }
    public void GoalDestroyed()
    {
        if (goalDestroyed != null)
            goalDestroyed();
    }
    public void OnAttack(IHasHealth entity, float basedmg)
    {
        if (Attack != null)
            Attack(entity, basedmg);
    }

    public void OnSkill(Skills skill)
    {
        if (ActivateSkill != null)
        {
            Debug.Log("Skill: " + skill.name + " activated");
            ActivateSkill(skill);
        }
    }


    public void OnSkillDeactivation(Skills skill)
    {
        if (DeactivateSkill != null)
        {
            Debug.Log("Skill: " + skill.name + " deactivated");
            DeactivateSkill(skill);
        }
    }

    public void OnSnare()
    {
        if (Snare == null)
        {
           Debug.LogError("No Snare event");
        }
        else
        {
            Snare();
        }
    }

    public void OnHighHat()
    {
        if (HighHat == null)
        {
           Debug.LogError("No Highhat event");
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
           Debug.LogError("No OnKick event");
        }
        else
        {
            Kick();
        }
    }

    public void OnDeactivate()
    {
        if (Deactivate == null)
        {
            Debug.Log("KickEvent has no subscriber");
        }
        else
        {
            Deactivate();
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