using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private void Start()
    {
        EventSystem.instance.Attack += DoDamage;
    }
    
    private void OnDisable()
    {
        EventSystem.instance.Attack -= DoDamage;
    }
    
    public void DoDamage(IHasHealth entity, float baseDmg)
    {
        entity.TakeDamage(baseDmg);
    }
}
