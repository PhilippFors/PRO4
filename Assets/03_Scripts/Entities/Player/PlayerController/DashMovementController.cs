using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMovementController
{
    private float timeStartDash, currentDashValueTime, timeSinceStarted, actualDashDistance, frametime = 0.0f, delayCountdown;
    public bool isDashing = false, dashDelayOn = false;
    public GameObject _child;


    private Vector3 velocity;
    public DashMovementController(PlayerStateMachine controller)
    {
        _child = controller.transform.GetChild(0).gameObject;
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
        if (controller.dashValue < 100 || controller.currentMoveDirection == Vector3.zero)
            return;

        controller.checkEnemy = true;
        controller.dashValue = 0f;
        //_child.GetComponent<Animator>().SetTrigger("Dash");

        velocity = Vector3.Scale(controller.currentMoveDirection.normalized, controller.dashDistance * new Vector3((Mathf.Log
        (1f / (controller.deltaTime * controller.rb.drag + 1)) / -controller.deltaTime),
        controller.transform.position.y,
        (Mathf.Log(1f / (controller.deltaTime * controller.rb.drag + 1)) / -controller.deltaTime)));

        CheckDashPathForEnemys(controller);
        //disable Hurtbox
        // rb.AddForce(velocity * dashForce, ForceMode.VelocityChange);

    }
    void DashUpdate(PlayerStateMachine controller)
    {

        controller.rb.velocity = velocity * controller.dashForce;
        velocity.x /= 1 + controller.rb.drag * controller.deltaTime;
        velocity.z /= 1 + controller.rb.drag * controller.deltaTime;

        //invincible while frametime not zero
        if (frametime <= 0 && !dashDelayOn)
        {
            //enable Hurtbox
            frametime = controller.dashDuration;
            dashDelayOn = true;
            controller.checkEnemy = false;
            currentDashValueTime = Time.time;
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
            controller.SetState(PlayerMovmentSate.standard);
        }
    }

    void CheckDashPathForEnemys(PlayerStateMachine controller)
    {
        controller.RayEmitter.forward = controller.currentMoveDirection.normalized;
        actualDashDistance = Vector3.Distance(controller.transform.position, controller.transform.position + controller.currentMoveDirection + ((velocity + velocity) / 2) * controller.dashDuration);

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
        else
        {
            Debug.Log("No enemie in sight!");
        }
    }

    public void DashCooldown(PlayerStateMachine controller)
    {
        float timeSinceDashEnded = controller.time - currentDashValueTime;

        float perc = timeSinceDashEnded / controller.dashValueTime;

        controller.dashValue = Mathf.Lerp(0, controller.maxDashValue, perc);
    }
}
