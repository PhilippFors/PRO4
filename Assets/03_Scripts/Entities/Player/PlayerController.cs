using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables


    #region __________Vector 3__________

    public Vector3 currentMoveDirection, currentLookDirection;
    private Vector3 forward, right, velocity, pointToLook;

    private Vector2 move;



    #endregion

    #region __________bool__________

    private bool isAiming, mouseused, gamepadused;
    public bool isDashing = false, isGrounded = true, dashDelayOn = false;
    public bool checkEnemy = false;

    #endregion

    #region __________float__________

    // [SerializeField] private float rotationSpeed = 50f; //later used for smoothing rapid turns of the player
    [SerializeField] private float moveSpeed = 5.0f, dashForce = 1.0f, dashDuration = 0.3f, dashDistance = 7f, drag = 1f, delayTime;
    private float timeStartDash, currentDashValueTime, frametime = 0.0f, timeSinceStarted, delayCountdown;
    public float dashValue, dashValueTime, maxDashValue;
    float actualDistance;


    #endregion

    #region __________others__________

    PlayerControls input;
    [HideInInspector] public Camera mainCam => GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    Plane groundPlane;
    private Rigidbody rb => GetComponent<Rigidbody>();

    LayerMask enemyMask => LayerMask.GetMask("Enemy");
    LayerMask groundMask => LayerMask.GetMask("Ground");

    public Transform RayEmitter;
    public Transform Ray1;
    public Transform Ray2;
    public Transform Ray3;

    GameObject dashTarget;
    #endregion


    #endregion

    #region enable/diable input

    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }
    #endregion

    #region Update/Start/Awake

    private void Awake()
    {
        input = new PlayerControls();
        input.Gameplay.Dash.performed += ctx => Dash();
        // input.Gameplay.Movement.performed += ctx => move = ctx.ReadValue<Vector2>();
        // input.Gameplay.Movement.canceled += ctx => move = Vector2.zero;
        input.Gameplay.Rotate.performed += ctx => GamepadLook(ctx.ReadValue<Vector2>());
        input.Gameplay.Look.performed += ctx => MouseLook(ctx.ReadValue<Vector2>());
    }

    void Start()
    {
        //Set up for the movement
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        Cursor.visible = true;
        frametime = dashDuration;
        delayCountdown = delayTime;

    }


    private void Update()
    {
        switch (isDashing)
        {
            case false:
                Move();
                break;
            case true:
                DashUpdate();
                break;
        }

        DashCoolDown();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + currentMoveDirection.normalized * moveSpeed * Time.fixedDeltaTime);
        if (isDashing)
            frametime -= Time.fixedDeltaTime;
    }

    #endregion

    #region Movement

    void Move()
    {
        IsGrounded();
        Vector2 move = input.Gameplay.Movement.ReadValue<Vector2>();
        Vector3 direction = new Vector3(move.x, 0, move.y);

        Vector3 horizMovement = right * direction.x;
        Vector3 vertikMovement = forward * direction.z;

        currentMoveDirection = horizMovement + vertikMovement;
    }

    void IsGrounded()
    {
        if (Physics.CheckSphere(transform.position, 1.1f, groundMask, QueryTriggerInteraction.Ignore))
        {
            rb.drag = drag;
            isGrounded = true;
        }
        else
        {
            rb.drag = 0;
            isGrounded = false;
        }
    }

    #endregion
    // private void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireSphere(posBeforDash, 0.2f);
    //     Gizmos.DrawWireSphere(posAfterDash, 0.2f);
    //     Gizmos.DrawWireSphere(transform.position + currentMoveDirection + ((velocity + velocity) / 2) * startDashTime, 0.4f);
    // }
    #region Dash
    public void Dash()
    {

        if (dashValue < 100 || currentMoveDirection == Vector3.zero)
            return;

        isDashing = true;
        checkEnemy = true;
        dashValue = 0f;

        velocity = Vector3.Scale(currentMoveDirection.normalized, dashDistance * new Vector3((Mathf.Log
        (1f / (Time.deltaTime * rb.drag + 1)) / -Time.deltaTime),
        transform.position.y,
        (Mathf.Log(1f / (Time.deltaTime * rb.drag + 1)) / -Time.deltaTime)));

        CheckForEnemy();
        //disable Hurtbox
        // rb.AddForce(velocity * dashForce, ForceMode.VelocityChange);
    }



    void DashUpdate()
    {

        rb.velocity = velocity * dashForce;
        velocity.x /= 1 + rb.drag * Time.deltaTime;
        velocity.z /= 1 + rb.drag * Time.deltaTime;

        //invincible while frametime not zero
        if (frametime <= 0 && !dashDelayOn)
        {
            //enable Hurtbox
            frametime = dashDuration;
            dashDelayOn = true;
            checkEnemy = false;
            currentDashValueTime = Time.time;
        }

        if (!dashDelayOn)
            return;

        DashDelay();
    }

    void DashDelay()
    {
        delayCountdown -= Time.deltaTime;
        currentMoveDirection = Vector3.zero;
        if (delayCountdown <= 0)
        {
            velocity = Vector3.zero;
            delayCountdown = delayTime;
            isDashing = false;
            dashDelayOn = false;
        }
    }

    void CheckForEnemy()
    {
        RayEmitter.forward = currentMoveDirection;
        actualDistance = Vector3.Distance(transform.position, transform.position + currentMoveDirection + ((velocity + velocity) / 2) * dashDuration);
        RaycastHit hit;
        if (Physics.Raycast(Ray1.position, Ray1.forward, out hit, actualDistance - 1f, enemyMask, QueryTriggerInteraction.Ignore) ||
        Physics.Raycast(Ray2.position, Ray2.forward, out hit, actualDistance - 1f, enemyMask, QueryTriggerInteraction.Ignore) ||
        Physics.Raycast(Ray3.position, Ray3.forward, out hit, actualDistance - 1f, enemyMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.transform.gameObject.GetComponent<EnemyBody>() != null)
            {
                dashTarget = hit.transform.gameObject;
                dashTarget.GetComponent<DisableCols>().Disable();
            }
        }
    }

    void DashCoolDown()
    {
        float timeSinceDashEnded = Time.time - currentDashValueTime;

        float perc = timeSinceDashEnded / dashValueTime;

        dashValue = Mathf.Lerp(0, maxDashValue, perc);
    }

    #endregion

    #region Look direction

    void GamepadLook(Vector2 v)
    {
        // if (input.Gameplay.Rotate.triggered || gamepadused)
        // {
        //     gamepadused = true;
        //     mouseused = false;

        var lookRot = mainCam.transform.TransformDirection(new Vector3(v.x, 0, v.y));
        pointToLook = Vector3.ProjectOnPlane(lookRot, Vector3.up);
        UpdateLookDirection();
        // }
    }

    void MouseLook(Vector2 v)
    {
        //     if (mouseused)
        //     {
        //         gamepadused = false;
        //         mouseused = true;
        //Creating a "mathematical" plane for the raycast to intersect with
        groundPlane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));
        //creating the Ray
        Ray cameraRay = mainCam.ScreenPointToRay(v);
        float rayLength;
        //checking if the raycast intersects with the plane
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 rayPoint = cameraRay.GetPoint(rayLength);
            //Debug.DrawLine(cameraRay.origin, rayPoint);
            pointToLook = rayPoint - transform.position;
        }
        UpdateLookDirection();
        // }
    }

    void UpdateLookDirection()
    {
        pointToLook.y = 0;
        if (pointToLook != Vector3.zero)
        {
            Quaternion newRot = Quaternion.LookRotation(pointToLook);
            transform.rotation = newRot;
            currentLookDirection = newRot.eulerAngles;
            //Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * rotationSpeed);
        }
    }

    #endregion
}
