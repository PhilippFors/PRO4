﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MultiplierName
{
    defense,
    damageMod,

    speedMod

}
public struct Multiplier
{
    private readonly float RESET_VALUE;
    private MultiplierName _name;
    private float v;
    public Multiplier(float value, float resetValue, MultiplierName name)
    {
        v = value;
        RESET_VALUE = resetValue;
        _name = name;
    }

    //Adding functionality to the individual Modifiers can make using it easier
    public void ResetMod()
    {
        v = RESET_VALUE;
    }
    public float GetModValue()
    {
        return v;
    }

    public void SetMod(float value)
    {
        v = value;
    }

    public MultiplierName GetName()
    {
        return _name;
    }
}
