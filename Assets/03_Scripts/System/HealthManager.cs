using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public ModifierManager modManager;
    public float calcDmg(float baseDmg, float initHealth)
    {
        return initHealth - baseDmg * modManager.getModifier();
    }
}
