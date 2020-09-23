﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMovementController
{
    private float timeStartDash, timeSinceStarted, actualDashDistance, frametime = 0.0f, delayCountdown;
    public bool isDashing = false, dashDelayOn = false;
    private Vector3 velocity;
    AnimationController animCon => GameObject.FindGameObjectWithTag("Player").GetComponent<AnimationController>();
    AttackStateMachine _attackStateMachine => GameObject.FindGameObjectWithTag("Player").GetComponent<AttackStateMachine>();

    public DashMovementController(PlayerStateMachine controller)
    {
        frametime = controller.dashDuration;
        delayCountdown = controller.delayTime;
    }

    public void Tick(PlayerStateMachine controller)
    {
        DashUpdate(controller);
        frametime -= controller.deltaTime;
    }
    public void DashInit(PlayerStateMachine controller)
    {

        GetCurrentMovedirection(controller);

        if (controller.dashCharge < controller.maxDashCharge || !controller.isMoving)
            return;
    	
        velocity = Vector3.zero;
        controller.selfCol.enabled = false;
        animCon.Dasher();
        _attackStateMachine.SetBase();
        controller.checkEnemy = true;
        controller.dashCharge = 0f;

        velocity = Vector3.Scale(Vector3.Normalize(controller.currentMoveDirection + controller.velocity), controller.dashDistance * new Vector3((Mathf.Log
        (1f / (controller.deltaTime * controller.drag + 1)) / -controller.deltaTime),
        controller.transform.position.y,
        (Mathf.Log(1f / (controller.deltaTime * controller.drag + 1)) / -controller.deltaTime)));

        CheckDashPathForEnemys(controller);

        //disable Hurtbox
        // rb.AddForce(velocity * dashForce, ForceMode.VelocityChange);
    }

    void GetCurrentMovedirection(PlayerStateMachine controller)
    {
        Vector2 move = controller.input.Gameplay.Movement.ReadValue<Vector2>();
        Vector3 direction = new Vector3(move.x, 0, move.y);
        Vector3 horizMovement = controller.right * direction.x;
        Vector3 vertikMovement = controller.forward * direction.z;
        controller.currentMoveDirection = horizMovement + vertikMovement;
    }

    void DashUpdate(PlayerStateMachine controller)
    {

        controller.velocity = velocity * controller.dashForce;
        velocity.x /= 1 + controller.drag * controller.deltaTime;
        velocity.z /= 1 + controller.drag * controller.deltaTime;

        //invincible while frametime not zero
        if (frametime <= 0 && !dashDelayOn)
        {
            //enable Hurtbox
            frametime = controller.dashDuration;
            dashDelayOn = true;
            controller.selfCol.enabled = true;
            controller.dashTime = Time.time;
        }

        if (!dashDelayOn)
            return;

        DashDelay(controller);

    }

    void DashDelay(PlayerStateMachine controller)
    {
        delayCountdown -= controller.deltaTime;
        controller.currentMoveDirection = Vector3.zero;
        if (delayCountdown <= 0)
        {
            velocity = Vector3.zero;
            delayCountdown = controller.delayTime; ;
            dashDelayOn = false;
            animCon.MoveStarter();
            controller.SetState(PlayerMovementSate.standard);
            // controller.characterController.detectCollisions = true;
        }
    }

    void CheckDashPathForEnemys(PlayerStateMachine controller)
    {
        controller.RayEmitter.forward = controller.currentMoveDirection.normalized;
        actualDashDistance = Vector3.Distance(controller.transform.position, controller.transform.position + controller.currentMoveDirection + ((velocity + velocity) / 2) * controller.dashDuration);
        // controller.characterController.detectCollisions = false;
        
        RaycastHit[] cols = Physics.SphereCastAll(controller.RayEmitter.position, 2f, controller.RayEmitter.forward, actualDashDistance, controller.enemyMask, QueryTriggerInteraction.Ignore);
        if (cols != null)
        {
            foreach (RaycastHit hits in cols)
            {
                if (hits.transform.gameObject.GetComponent<EnemyBody>() != null)
                {
                    controller.dashTarget = hits.transform.gameObject;
                    controller.dashTarget.GetComponent<DisableCols>().Disable();
                }
            }
        }
        controller.checkEnemy = false;
    }
}
