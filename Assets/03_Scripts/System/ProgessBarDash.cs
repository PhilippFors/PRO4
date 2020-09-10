﻿using System.Collections;
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
        maximum = dash.maxDashValue;
        current = dash.dashValue;
        float fillAmount = (float) current / (float) maximum;
        slider.value = fillAmount;
    }
}
