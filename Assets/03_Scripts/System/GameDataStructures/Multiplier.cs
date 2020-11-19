using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MultiplierName
{
    defense,
    damage,
    speed,

    health,
    stunMultiplier

}
[Author(mainAuthor = "Philipp Forstner")]
public class Multiplier
{
    private readonly float RESET_VALUE;
    private MultiplierName _name;
    private float v;
    public Multiplier(float value,  MultiplierName name, float resetValue = 1.0f)
    {
        v = value;
        RESET_VALUE = resetValue;
        _name = name;
    }

    //Adding functionality to the individual Modifiers can make using it easier
    public void ResetMultiplier()
    {
        v = RESET_VALUE;
    }
    public float GetValue()
    {
        return v;
    }

    public void SetValue(float value)
    {
        v = value;
    }

    public MultiplierName GetName()
    {
        return _name;
    }
}
