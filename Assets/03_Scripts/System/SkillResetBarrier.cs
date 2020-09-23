using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillResetBarrier : MonoBehaviour
{

    AttackStateMachine body;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        AttackStateMachine obj = other.GetComponent<AttackStateMachine>();
        if (obj != null)
            body = obj;

        if (other.gameObject.GetComponent<PlayerBody>())
        {
            body.SetAllSkillsToZero();
        }

            
    }
}
