using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStats : IHasHealth
{
    List<GameStatistics> statList { get; set; }
    void InitStats();
    void SetStatValue(StatName name, float value);
    float GetStatValue(StatName stat);
}
