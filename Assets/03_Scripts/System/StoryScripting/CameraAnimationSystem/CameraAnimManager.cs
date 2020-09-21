using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimManager : MonoBehaviour
{
    public GameObject cam;
    public PlayerBody player;

    private void Start()
    {
        MyEventSystem.instance.notifyCamManager += StartAnim;
    }
    public void StartAnim(Transform endposition, Transform playerDest)
    {
        MyEventSystem.instance.StartCamAnim(cam.transform, endposition, player.transform, playerDest);
    }

    public void ResetCAm()
    {
        cam.transform.position = new Vector3(0, 0, 0);
    }
}
