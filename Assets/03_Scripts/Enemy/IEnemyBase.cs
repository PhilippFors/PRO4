using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyBase
{
    void setHealth(float health);

    float getHealth();

    float getSpeed();

    float getRange();

    float getBaseDmg();

    float getTurnSpeed();
}
