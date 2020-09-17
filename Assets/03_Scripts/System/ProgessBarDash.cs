using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgessBarDash : ProgessBar
{
    public Slider slider;
    public PlayerStateMachine dash;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    public void GetCurrentFill()
    {
        maximum = dash.maxDashCharge;
        current = dash.dashCharge;
        float fillAmount = (float) current / (float) maximum;
        slider.value = fillAmount;
    }
}
