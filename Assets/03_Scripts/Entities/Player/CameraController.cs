using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player => GameObject.FindGameObjectWithTag("Player");
    private Vector3 playerPosition;
    
    private PlayerStateMachine playercontrols;
    public float cameraFollowSpeed = 5f;

    private void Start() {
        playercontrols = player.GetComponent<PlayerStateMachine>();
    }
    private void FixedUpdate()
    {
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) + playercontrols.currentMoveDirection* 1.05f;
        transform.position = Vector3.Lerp(transform.position, playerPosition, Time.fixedDeltaTime*cameraFollowSpeed);
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }
}
