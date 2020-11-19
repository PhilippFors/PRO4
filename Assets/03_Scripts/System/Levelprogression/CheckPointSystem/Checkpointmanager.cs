using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author(mainAuthor = "Philipp Forstner")]
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

    public void TransportToCheckpoint(PlayerStatistics body)
    {
        GameManager.instance.Respawn(body, transform.TransformPoint(currentCheck.transform.position));
    }

}
