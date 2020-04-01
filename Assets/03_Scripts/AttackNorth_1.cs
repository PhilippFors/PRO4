using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNorth_1 : MonoBehaviour
{
    //außen: 559
    //innen: 568
    // Start is called before the first frame update

    private bool isOut;
    private bool slow;
    float time;

    void Start()
    {
        isOut = false;
        slow = false;
        time = 0.3f;
        StartCoroutine("Pulse");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (slow == false)
            {
                time = 1;
                slow = true;
            } else
            {
                time = 0.3f;
                slow = false;
            }
        }
    }

    IEnumerator Pulse()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(time);
        if (isOut == false)
        {
            transform.position = transform.position + new Vector3(-9, 0, 0);
            isOut = true;
            StartCoroutine("Pulse");
        } else
        {
            transform.position = transform.position + new Vector3(9, 0, 0);
            isOut = false;
            StartCoroutine("Pulse");
        }

    }
}
