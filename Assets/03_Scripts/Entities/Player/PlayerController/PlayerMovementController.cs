﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController :MonoBehaviour

{
    private Plane groundPlane;
    private Vector3 forward, right, pointToLook;
    private Camera mainCam => GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    #region Update/Start/Awake

    public void Tick(PlayerStateMachine controller)
    {
        Move(controller);
        GamepadLook(controller);
        MouseLook(controller);
    }

    private void Start()
    {
        //Set up for the movement
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        Cursor.visible = true;
    }

    #endregion

    #region Movement

    void Move(PlayerStateMachine controller)
    {
        IsGrounded(controller);
        Vector2 move = controller.move;
        Vector3 direction = new Vector3(move.x, 0, move.y);

        Vector3 horizMovement = right * direction.x;
        Vector3 vertikMovement = forward * direction.z;

        controller.currentMoveDirection = horizMovement + vertikMovement;
    }

    void IsGrounded(PlayerStateMachine controller)
    {
        if (Physics.CheckSphere(controller.transform.position, 1.1f, controller.groundMask, QueryTriggerInteraction.Ignore))
        {
            controller.rb.drag = controller.drag;
            controller.isGrounded = true;
        }
        else
        {
            controller.rb.drag = 0;
            controller.isGrounded = false;
        }
    }

    #endregion

    #region Look direction

    void GamepadLook(PlayerStateMachine controller)
    {
        if (controller.input.Gameplay.Rotate.triggered || controller.gamepadused)
        {
            controller.gamepadused = true;
            controller.mouseused = false;
            Vector2 v = controller.gamepadRotate;
            var lookRot = mainCam.transform.TransformDirection(new Vector3(v.x, 0, v.y));
            pointToLook = Vector3.ProjectOnPlane(lookRot, Vector3.up);
            UpdateLookDirection(controller);
        }
    }

    void MouseLook(PlayerStateMachine controller)
    {
        if (controller.input.Gameplay.Look.triggered || controller.mouseused)
        {
            controller.gamepadused = false;
            controller.mouseused = true;
            Vector2 v = controller.mouseLook;
            //Creating a "mathematical" plane for the raycast to intersect with
            groundPlane = new Plane(Vector3.up, new Vector3(0, controller.transform.position.y, 0));
            //creating the Ray
            Ray cameraRay = mainCam.ScreenPointToRay(v);
            float rayLength;
            //checking if the raycast intersects with the plane
            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 rayPoint = cameraRay.GetPoint(rayLength);
                //Debug.DrawLine(cameraRay.origin, rayPoint);
                pointToLook = rayPoint - controller.transform.position;
            }
            UpdateLookDirection(controller);
        }
    }

    void UpdateLookDirection(PlayerStateMachine controller)
    {
        pointToLook.y = 0;
        if (pointToLook != Vector3.zero)
        {
            Quaternion newRot = Quaternion.LookRotation(pointToLook);
            controller.transform.rotation = newRot;
            controller.currentLookDirection = newRot.eulerAngles;
            //Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * rotationSpeed);
        }
    }

    #endregion
}
