using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author(mainAuthor = "Philipp Forstner")]
public class SkillPlate : MonoBehaviour
{
    public float chargeAmount;
    bool chargeSkill = false;
    AttackStateMachine body;
    float currentChargeF = 0;
    public float maxChargeF = 30f;
    bool canCharge = true;

    public float nextChargeCountdown = 10f;
    private void OnTriggerEnter(Collider other)
    {
        AttackStateMachine obj = other.GetComponent<AttackStateMachine>();
        if (obj != null)
            body = obj;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerBody>())
            if (canCharge)
                chargeSkill = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerBody>())
        {
            chargeSkill = false;
            body = null;
        }
    }

    private void Update()
    {
        if (chargeSkill & body != null)
        {
            float h = chargeAmount * Time.deltaTime;

            bool increased = true;
            body.IncreaseAllSkills(h, out increased);

            if (increased)
                if (currentChargeF >= maxChargeF)
                {
                    chargeSkill = false;
                    canCharge = false;
                    StartCoroutine(ChargeDelay());
                }
        }
    }

    IEnumerator ChargeDelay()
    {
        currentChargeF = 0;
        yield return new WaitForSeconds(nextChargeCountdown);
        canCharge = true;
        chargeSkill = true;
    }
}
