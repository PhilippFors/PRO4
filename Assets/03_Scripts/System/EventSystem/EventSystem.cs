using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{
    public event System.Action ResetMult;
    public event Action<IHasHealth, float> Attack;
    public event Action<MultiplierName, float> ActivateSkill;

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
