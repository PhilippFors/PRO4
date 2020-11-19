using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author(mainAuthor = "Philipp Forstner")]
public class HealthPlate : MonoBehaviour
{
    public float healAmount;
    bool heal = false;
    PlayerStatistics body;
    float currentHealfactor = 0;
    public float maxHealfactor = 30f;
    bool canHeal = true;

    public float nextHealCountdown = 10f;
    private void OnTriggerEnter(Collider other)
    {
        PlayerStatistics obj = other.GetComponent<PlayerStatistics>();
        if (obj != null)
            body = obj;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerStatistics>())
        {
            if (canHeal)
                heal = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerStatistics>())
        {
            heal = false;
            body = null;
        }

    }

    private void Update()
    {
        if (heal & body != null)
        {
            float h = healAmount * Time.deltaTime;
            body.Heal(h);
            if (body.currentHealth.Value < body.GetStatValue(StatName.MaxHealth))
                currentHealfactor += h;

            if (currentHealfactor >= maxHealfactor)
            {
                heal = false;
                canHeal = false;
                StartCoroutine(HealDelay());
            }
        }
    }

    IEnumerator HealDelay()
    {
        currentHealfactor = 0;
        yield return new WaitForSeconds(nextHealCountdown);
        canHeal = true;
        heal = true;
    }


}
