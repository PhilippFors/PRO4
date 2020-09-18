using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerAnimTrigger : MonoBehaviour
{
    public Transform playerDest;

    public Transform startPosition;
    public Transform endPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerBody>())
            EventSystem.instance.NotifyCamManager(endPosition, playerDest);
    }
}
