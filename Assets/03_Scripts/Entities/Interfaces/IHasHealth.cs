using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasHealth
{
   void TakeDamage(float damage);
   void Heal(float healAmount);
   void OnDeath();
}
