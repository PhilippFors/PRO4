﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 playerPosition;
    public float cameraFollowSpeed = 5f;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void LateUpdate()
    {
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, playerPosition, Time.deltaTime*cameraFollowSpeed);
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }
}
