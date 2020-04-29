using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeMovementController
{
    private float moveSpeed = 5.0f;
    private Camera mainCam => GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    private GameObject target;
    private Vector3 currentMoveDirection;
    private Vector3 forward, right;

    #region Update/Start/Awake

    public GrenadeMovementController(PlayerStateMachine controller)
    {
        //Set up for the movement
        forward = mainCam.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        Cursor.visible = true;

        //target = 
    }
    public void Tick(PlayerStateMachine controller)
    {
        Move(controller);
    }
    
    #endregion

    #region Movement

    void Move(PlayerStateMachine controller)
    {
       
        Vector2 move = controller.gamepadRotate;
        Vector3 direction = new Vector3(move.x, 0, move.y);

        Vector3 horizMovement = right * direction.x;
        Vector3 vertikMovement = forward * direction.z;

        currentMoveDirection = horizMovement + vertikMovement;
        target.transform.position = target.transform.position + currentMoveDirection * moveSpeed * Time.deltaTime;
    }
    

    #endregion

   
}
