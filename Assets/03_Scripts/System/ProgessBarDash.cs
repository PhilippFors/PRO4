using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgessBarDash : ProgessBar
{
    public Slider slider;
    public PlayerStateMachine dash;

    void Update()
    {
        GetCurrentFill();
    }

    public override void GetCurrentFill()
    {
        maximum = dash.maxDashCharge;
        current = dash.dashCharge;
        float fillAmount = (float) current / (float) maximum;
        slider.value = fillAmount;
    }
}
