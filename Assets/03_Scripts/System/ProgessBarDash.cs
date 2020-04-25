using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgessBarDash : ProgessBar
{
    
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

    public override void GetCurrentFill()
    {
        maximum = dash.maxDashValue;
        current = dash.dashValue;
        base.GetCurrentFill();
    }
}
