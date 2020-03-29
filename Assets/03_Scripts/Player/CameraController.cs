using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 playerPosition;
    public float cameraFollowSpeed = 2f;
    private void Update()
    {
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        transform.position = Vector3.Slerp(transform.position, playerPosition, Time.deltaTime*cameraFollowSpeed);
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }
}
