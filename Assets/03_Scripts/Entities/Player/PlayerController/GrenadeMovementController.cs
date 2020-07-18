using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeMovementController
{
    private Plane groundPlane;

    private float targetMoveSpeed = 15.0f;
    private Camera mainCam => GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    private GameObject target;
    private Vector3 targetCurrentMoveDirection;
    private Vector3 targetForward, targetRight;

    #region Update/Start/Awake

    public GrenadeMovementController(PlayerStateMachine controller)
    {
        //Set up for the movement
        targetForward = mainCam.transform.forward;
        targetForward.y = 0;
        targetForward = Vector3.Normalize(targetForward);
        targetRight = Quaternion.Euler(new Vector3(0, 90, 0)) * targetForward;
        Cursor.visible = true;

        controller.forward = mainCam.transform.forward;
        controller.forward.y = 0;
        controller.forward = Vector3.Normalize(controller.forward);
        controller.right = Quaternion.Euler(new Vector3(0, 90, 0)) * controller.forward;
        Cursor.visible = true;
    }

    public void Tick(PlayerStateMachine controller)
    {
        Move(controller);
        TargetMove(controller);
        TargetPosition(controller);
    }

    #endregion

    #region Movement

    void Move(PlayerStateMachine controller)
    {
        IsGrounded(controller);

        Vector2 move = controller.move;
        Vector3 direction = new Vector3(move.x, 0, move.y);

        Vector3 horizMovement = controller.right * direction.x;
        Vector3 vertikMovement = controller.forward * direction.z;

        controller.currentMoveDirection = horizMovement + vertikMovement;
    }

    void TargetMove(PlayerStateMachine controller)
    {
        if (controller.input.Gameplay.Rotate.triggered || controller.gamepadused)
        {
            controller.gamepadused = true;
            controller.mouseused = false;

            target = controller.target.target;
            Vector2 targetMove = controller.gamepadRotate;
            Vector3 targetDirection = new Vector3(targetMove.x, 0, targetMove.y);

            Vector3 targetHorizMovement = targetRight * targetDirection.x;
            Vector3 targetVertikMovement = targetForward * targetDirection.z;

            targetCurrentMoveDirection = targetHorizMovement + targetVertikMovement;
            target.transform.position = 
                target.transform.position + targetCurrentMoveDirection * targetMoveSpeed * Time.deltaTime;
        }
    }

    public void TargetPosition(PlayerStateMachine controller)
    {
        if (controller.input.Gameplay.Look.triggered || controller.mouseused)
        {
            controller.gamepadused = false;
            controller.mouseused = true;
            target = controller.target.target;
            Vector3 temp = MousePosition();
            target.transform.position = new Vector3(temp.x, 1, temp.z);
        }
    }

    public Vector3 MousePosition()
    {
        groundPlane = new Plane(Vector3.up, 0f);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float dist;
        if (groundPlane.Raycast(ray, out dist))
        {
            return ray.GetPoint(dist);
        }

        return Vector3.zero;
    }

    #endregion


    void IsGrounded(PlayerStateMachine controller)
    {
        if (Physics.CheckSphere(controller.transform.position, 1.1f, controller.groundMask,
            QueryTriggerInteraction.Ignore))
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
}