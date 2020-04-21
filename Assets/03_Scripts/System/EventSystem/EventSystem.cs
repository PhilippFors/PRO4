using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{
    public event System.Action ResetMult;
    public event Action<IHasHealth, float> Attack;
    public event Action<MultiplierName, float> ActivateSkill;

    //Events die von der Musik ausgelöst werden
    public event System.Action Kick;
    public event System.Action Bass;


    public static EventSystem instance;

    private void Awake()
    {
        instance = this;
    }

    public void OnAttack(IHasHealth entity, float basedmg)
    {
        Attack(entity, basedmg);
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
        if (Kick == null)
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
