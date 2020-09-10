using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgessBar: MonoBehaviour
{
    public float maximum ;

    public float current;

    public Image image;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void GetCurrentFill()
    {
        
        float fillAmount = (float) current / (float) maximum;
        image.fillAmount = fillAmount;
    }
}
