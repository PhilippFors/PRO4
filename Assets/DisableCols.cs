using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCols : MonoBehaviour
{
    float timeSinceDashEnd;
    public float reenableColliderTime;
    public bool checkForExit = false;
    private void Update()
    {

        if (!checkForExit)
            return;

        CheckForExit();
    }
    public void Disable()
    {
        GetComponent<CapsuleCollider>().isTrigger = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        checkForExit = true;
    }

    void CheckForExit()
    {
        timeSinceDashEnd += Time.deltaTime;
        if (timeSinceDashEnd >= reenableColliderTime)
        {
            GetComponent<CapsuleCollider>().isTrigger = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

            timeSinceDashEnd = 0f;
            checkForExit = false;
        }
    }
}
