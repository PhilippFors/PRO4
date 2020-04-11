using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;

public class ProgressBarLowPass : ProgessBar
{
    [FormerlySerializedAs("skills")] public PlayerAttack player;

    public int id;
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
        maximum = player.skill[id].max;
        current = player.skill[id].current;
        base.GetCurrentFill();
    }
}
