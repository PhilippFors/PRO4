﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStats
{
    List<GameStatistics> statList { get; set; }
    void InitStats(StatTemplate template);
    void SetStatValue(StatName name, float value);
    float GetStatValue(StatName stat);
}
