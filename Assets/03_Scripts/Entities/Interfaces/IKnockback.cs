using System.Collections;
using UnityEngine;
public interface IKnockback
{
    Coroutine stunCoroutine {get; set;}
    float currentStun {get; set;}
    void ApplyKnockback(float force);

    void ApplyStun(float stun);

    IEnumerator StunCooldown();
}
