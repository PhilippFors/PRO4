using UnityEngine;
using System;
using System.Collections;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills")]
public class Skills : ScriptableObject
{
    public string skillName;
    public float current;
    public float max;
    public float timer;
    public bool isActive;
    public int comboCounter;
    public float activeValue;
    public float deactiveValue;
    public float decreaseMultValue;
    public float increaseMultValue;
    public Symbol buffSymbol;
    public Symbol debuffSymbol;
}