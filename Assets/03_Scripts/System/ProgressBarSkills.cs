using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class ProgressBarSkills : ProgessBar
{
    public PlayerAttack player;

    //public int id = 0;
    public Skills skillObject;

    public RawImage upSkill;
    
    public RawImage downSkill;

    // Start is called before the first frame update
    void Start()
    {
        //skillObject = player.skills[id];
        SetSkillIcons();
    }

    public void SetSkillIcons()
    {
        upSkill.texture = skillObject.buffSymbol.tex;
        downSkill.texture = skillObject.debuffSymbol.tex;
    }


    // Update is called once per frame
    void Update()
    {
        this.GetCurrentFill();
    }

    public override void GetCurrentFill()
    {
        maximum = skillObject.max;
        current = skillObject.current;
        base.GetCurrentFill();
    }
}
