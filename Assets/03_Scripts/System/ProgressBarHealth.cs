using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarHealth : ProgessBar
{
    public PlayerStatistics player;

    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerStatistics>();
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }


    public override void GetCurrentFill()
    {
        maximum = (player.statTemplate.statList[2].Value);
        current = (player.currentHealth.Value);
        float fillAmount = (float) current / (float) maximum;
        slider.value = fillAmount;
    }
}