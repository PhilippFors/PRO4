using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarHealth : ProgessBar
{
    public PlayerBody player;

    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }


    public override void GetCurrentFill()
    {
        maximum = (player.template.statList[2].Value);
        current = (player.currentHealth.Value);
        float fillAmount = (float) current / (float) maximum;
        slider.value = fillAmount;
    }
}