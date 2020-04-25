using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMovementController : MonoBehaviour
{
    private float timeStartDash, currentDashValueTime,  timeSinceStarted,  actualDashDistance;
    public bool isDashing = false, dashDelayOn = false;
    

    private Vector3 velocity;

    public void Tick(PlayerStateMachine controller)
    {
        DashUpdate(controller);
        controller.frametime -= Time.deltaTime;
    }
    public void DashInit(PlayerStateMachine controller)
    {

        if (controller.dashValue < 100 || controller.currentMoveDirection == Vector3.zero)
            return;

        controller.checkEnemy = true;
        controller.dashValue = 0f;

        velocity = Vector3.Scale(controller.currentMoveDirection.normalized, controller.dashDistance * new Vector3((Mathf.Log
        (1f / (Time.deltaTime * controller.rb.drag + 1)) / -Time.deltaTime),
        controller.transform.position.y,
        (Mathf.Log(1f / (Time.deltaTime * controller.rb.drag + 1)) / -Time.deltaTime)));

        CheckDashPathForEnemys(controller);
        //disable Hurtbox
        // rb.AddForce(velocity * dashForce, ForceMode.VelocityChange);

    }
    void DashUpdate(PlayerStateMachine controller)
    {

        controller.rb.velocity = velocity * controller.dashForce;
        velocity.x /= 1 + controller.rb.drag * Time.deltaTime;
        velocity.z /= 1 + controller.rb.drag * Time.deltaTime;

        //invincible while frametime not zero
        if (controller.frametime <= 0 && !dashDelayOn)
        {
            //enable Hurtbox
            controller.frametime = controller.dashDuration;
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
        controller.delayCountdown -= Time.deltaTime;
        controller.currentMoveDirection = Vector3.zero;
        if (controller.delayCountdown <= 0)
        {
            velocity = Vector3.zero;
            controller.delayCountdown = controller.delayTime; ;
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
        float timeSinceDashEnded = Time.time - currentDashValueTime;

        float perc = timeSinceDashEnded / controller.dashValueTime;

        controller.dashValue = Mathf.Lerp(0, controller.maxDashValue, perc);
    }
}
