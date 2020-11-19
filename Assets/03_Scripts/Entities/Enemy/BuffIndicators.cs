using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Author("Philipp Forstner")]
public class BuffIndicators : MonoBehaviour
{
    public List<Light> lights = new List<Light>();
    public Color BuffColor;
    public Color DebuffColor;
    public Color StandardColor;
    public void BuffLights()
    {
        foreach (Light light in lights)
        {
            light.color = BuffColor;
        }
    }

    public void DebuffLights()
    {
        foreach (Light light in lights)
        {
            light.color = DebuffColor;
        }
    }

    public void StandardColors()
    {
        foreach (Light light in lights)
        {
            light.color = StandardColor;
        }
    }
}
