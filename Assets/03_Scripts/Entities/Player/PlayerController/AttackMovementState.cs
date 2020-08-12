using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMovementState
{
    private GameObject _child; //the weapon object
    private Plane groundPlane;
    private Quaternion currentRotation;
    private Camera mainCam => GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    private AttackStateMachine attack => GameObject.FindGameObjectWithTag("Player").GetComponent<AttackStateMachine>();
    public AttackMovementState(PlayerStateMachine controller)
    {
        _child = controller.transform.GetChild(0).gameObject; //first child object of the player

    }

    public void StopMovement(PlayerStateMachine controller)
    {
        controller.currentMoveDirection = Vector3.zero;
        currentRotation = controller.transform.rotation;
    }
    public void Tick(PlayerStateMachine controller)
    {

        /*if (_child.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("FastToIdle"))
        {
            controller.SetState(PlayerMovmentSate.standard);
        }*/
        if (attack.currentState.canTurn)
        {
            MouseLook(controller);
            GamepadLook(controller);
        }
        
    }
    
    void GamepadLook(PlayerStateMachine controller)
    {
        if (controller.input.Gameplay.Rotate.triggered || controller.gamepadused)
        {
            controller.gamepadused = true;
            controller.mouseused = false;
            Vector2 v = controller.gamepadRotate;
            var lookRot = mainCam.transform.TransformDirection(new Vector3(v.x, 0, v.y));
            controller.pointToLook = Vector3.ProjectOnPlane(lookRot, Vector3.up);
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
                controller.pointToLook = rayPoint - controller.transform.position;
            }
            UpdateLookDirection(controller);
        }
    }

    void UpdateLookDirection(PlayerStateMachine controller)
    {
        controller.pointToLook.y = 0;
        if (controller.pointToLook != Vector3.zero)
        {
            Quaternion newRot = Quaternion.LookRotation(controller.pointToLook);
            Vector3 temp = newRot.eulerAngles;
            //temp.y = Mathf.Clamp(temp.y, currentRotation.eulerAngles.y -attack.maxRot, currentRotation.eulerAngles.y + attack.maxRot);
           
            controller.transform.rotation = newRot;
            controller.currentLookDirection = temp;
            
            
            //Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * rotationSpeed);
        }
    }


}