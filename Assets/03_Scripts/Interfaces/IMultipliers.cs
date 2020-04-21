using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMultipliers
{
    List<Multiplier> multList { get; set; }

    void InitMultiplier();
    void SetMultValue(MultiplierName name, float value);
    float GetMultValue(MultiplierName name);
    void ResetMultipliers();

}
