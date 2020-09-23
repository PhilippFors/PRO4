using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCols : MonoBehaviour
{
    float enableCountdown;
    public float reenableColliderTime;
    public bool checkForExit = false;
    Rigidbody rb => GetComponent<Rigidbody>();
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
        GetComponent<CapsuleCollider>().enabled = false;
        rb.detectCollisions = false;
        rb.constraints = RigidbodyConstraints.FreezePosition;

        checkForExit = true;
    }

    void CheckForExit()
    {
        enableCountdown -= Time.deltaTime;
        if (enableCountdown <= 0)
        {
            GetComponent<CapsuleCollider>().enabled = true;
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.detectCollisions = true;
            enableCountdown = reenableColliderTime;
            checkForExit = false;
        }
    }
}
