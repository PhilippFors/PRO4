using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasHealth
{
   void CalculateHealth(float damage);

   void OnDeath();
}
