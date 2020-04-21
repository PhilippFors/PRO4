using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{
    public event System.Action ResetMult;
    public event Action<IHasHealth, float> Attack;
    public event Action<MultiplierName, float> ActivateSkill;


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
}
