using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMultipliers
{
    List<Multiplier> multList { get; set; }

    void AddMultiplier(MultiplierName name, float value, float time);
    float GetMultValue(MultiplierName name);
    void ResetMultipliers();

    IEnumerator MultiplierTimer(float time, int id);

}
