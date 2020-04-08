using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierManager : MonoBehaviour
{
    float Modifier = 1.0f;

    public void changeMod(float par)
    {
        Modifier *= par;
    }

    public float getModifier()
    {
        return Modifier;
    }
}
