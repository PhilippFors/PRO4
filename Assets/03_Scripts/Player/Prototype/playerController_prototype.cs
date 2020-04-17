using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController_prototype : MonoBehaviour
{
    #region Variables


    #region __________Vector 3__________

    public Vector3 currentMoveDirection, currentLookDirection, velocity;
    private Vector3 forward, right, moveVelocity, pointToLook;

    #endregion

    #region __________bool__________

    private bool isAiming, mouseused, gamepadused;
    public bool isDashing = false;

    #endregion

    #region __________float__________

    // [SerializeField] private float rotationSpeed = 50f; //later used for smoothing rapid turns of the player
    [SerializeField] private float moveSpeed = 5.0f;
    private float timeStartDash, currentDashValueTime, frametime = 0.0f;
    public float dashTime, dashValue, dashValueTime, maxDashValue, dashForce = 1.0f;
    public float dashDistance = 7f;

    #endregion

    #region __________others__________

    PlayerControls input;
    [HideInInspector] public Camera mainCam => GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    Plane groundPlane;
    private Rigidbody rb => GetComponent<Rigidbody>();

    #endregion


    #endregion

    #region controls enable/disable

    private void OnEnable()
    {
        input.Gameplay.Enable();
    }

    private void OnDisable()
    {

        input.Gameplay.Disable();
    }

    #endregion

    #region Update/Start/Awake

    private void Awake()
    {
        input = new PlayerControls();

        input.Gameplay.Dash.performed += ctx => Dash();
    }

    void Start()
    {
        //Set up for the movement
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        Cursor.visible = true;
    
    }


    private void Update()
    {
        GamepadLook();
        MouseLook();

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
        rb.MovePosition(rb.position + currentMoveDirection * Time.fixedDeltaTime);
    }

    #endregion

    #region Movement

    public void Move()
    {
        Vector2 move = input.Gameplay.Movement.ReadValue<Vector2>();
        Vector3 direction = new Vector3(move.x, 0, move.y);

        moveVelocity = direction * moveSpeed;
        Vector3 horizMovement = right * moveVelocity.x;
        Vector3 vertikMovement = forward * moveVelocity.z;

        currentMoveDirection = horizMovement + vertikMovement;
    }

    #endregion

    #region Dash
    public void Dash()
    {
        RaycastHit hit;
        if (dashValue < 100)
            return;


        isDashing = true;
        dashValue = 0f;
        if (Physics.Raycast(transform.position, currentMoveDirection, out hit, dashDistance))
        {
            if (hit.transform.GetComponent<EnemyBaseClass>() != null)
            {
                gameObject.GetComponent<BoxCollider>().isTrigger = true;
                rb.useGravity = false;
            }
        }

        velocity = Vector3.Scale(currentMoveDirection.normalized, dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * rb.drag + 1)) / -Time.deltaTime),
                                                                                              transform.position.y,
                                                                                             (Mathf.Log(1f / (Time.deltaTime * rb.drag + 1)) / -Time.deltaTime)));

        rb.AddForce(velocity * dashForce, ForceMode.VelocityChange);
        
    }

    public void DashUpdate()
    {
        frametime += Time.deltaTime;
        if (frametime >= 0.2f)
        {
            frametime = 0f;
            currentDashValueTime = Time.time;
            isDashing = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyBaseClass>() != null)
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            rb.useGravity = true;
        }
    }

    public void DashCoolDown()
    {
        float timeSinceDashEnded = Time.time - currentDashValueTime;

        float perc = timeSinceDashEnded / dashValueTime;

        dashValue = Mathf.Lerp(0, maxDashValue, perc);
    }

    #endregion

    #region Look direction

    public void GamepadLook()
    {
        if (input.Gameplay.Rotate.triggered || gamepadused)
        {
            gamepadused = true;
            mouseused = false;
            Vector2 rotate = input.Gameplay.Rotate.ReadValue<Vector2>();
            var lookRot = mainCam.transform.TransformDirection(new Vector3(rotate.x, 0, rotate.y));
            pointToLook = Vector3.ProjectOnPlane(lookRot, Vector3.up);
            UpdateLookDirection();
        }

    }

    public void MouseLook()
    {
        if (input.Gameplay.Look.triggered || mouseused)
        {
            gamepadused = false;
            mouseused = true;
            Vector2 rotate = input.Gameplay.Look.ReadValue<Vector2>();
            //Creating a "mathematical" plane for the raycast to intersect with
            groundPlane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));
            //creating the Ray
            Ray cameraRay = mainCam.ScreenPointToRay(rotate);
            float rayLength;
            //checking if the raycast intersects with the plane
            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 rayPoint = cameraRay.GetPoint(rayLength);
                //Debug.DrawLine(cameraRay.origin, rayPoint);
                pointToLook = rayPoint - transform.position;
            }
            UpdateLookDirection();
        }

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
