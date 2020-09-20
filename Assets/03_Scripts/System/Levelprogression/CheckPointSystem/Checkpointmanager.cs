using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpointmanager : MonoBehaviour
{
    public CheckPoint currentCheck;
    CheckPoint oldCheck;
    public Transform player;
    void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForEndOfFrame();
        LevelEventSystem.instance.checkPointReached += SetNewCheckPoint;
        LevelEventSystem.instance.returnToCheckpoint += TransportToCheckpoint;
    }

    void SetNewCheckPoint(CheckPoint point)
    {
        if (currentCheck != null)
        {
            currentCheck.isActive = false;
            oldCheck = currentCheck;
        }
        currentCheck = point;
        currentCheck.isActive = true;
    }

    public void TransportToCheckpoint(PlayerBody body)
    {
        GameManager.instance.Respawn(body, transform.TransformPoint(currentCheck.transform.position));
    }

}
