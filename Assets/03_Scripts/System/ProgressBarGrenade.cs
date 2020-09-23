using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarGrenade : ProgessBar
{
    public PlayerAttack player;

    //public int id = 0;

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
        maximum = player.greandeCooldown;
        current = player.currentGCooldown;
        base.GetCurrentFill();
    }
}
