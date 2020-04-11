using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class ProgressBarLowPass : ProgessBar
{
    public PlayerAttack skills;
    public float skill;
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
        maximum = skills.lowPassSkillMax;
        current = skills.lowPassSkill;
        base.GetCurrentFill();
    }
}
