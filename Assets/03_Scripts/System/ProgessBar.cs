using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class ProgessBar : MonoBehaviour
{
    public float maximum;
    public float current;
    public Image image;
    public virtual void GetCurrentFill()
    {
        float fillAmount = (float)current / (float)maximum;
        image.fillAmount = fillAmount;
    }
}
