using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IEnemyBase
{
    void SetStat(EnemyStatName stat, float value);

    float GetStat(EnemyStatName stat);
}
