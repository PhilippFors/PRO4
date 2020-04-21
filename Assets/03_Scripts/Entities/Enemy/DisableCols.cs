using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCols : MonoBehaviour
{
    float enableCountdown;
    public float reenableColliderTime;
    public bool checkForExit = false;

    private void Update()
    {

        if (!checkForExit)
            return;

        CheckForExit();
    }
    private void Start()
    {
        enableCountdown = reenableColliderTime;
    }
    public void Disable()
    {
        GetComponent<CapsuleCollider>().isTrigger = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        checkForExit = true;
    }

    void CheckForExit()
    {
        enableCountdown -= Time.deltaTime;
        if (enableCountdown <= 0)
        {
            GetComponent<CapsuleCollider>().isTrigger = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

            enableCountdown = reenableColliderTime;
            checkForExit = false;
        }
    }
}
