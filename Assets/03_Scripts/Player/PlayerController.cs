using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Variables
    PlayerControls input;
    [HideInInspector] public Camera mainCam => GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    [SerializeField] private float moveSpeed = 5f;
    // [SerializeField] private float rotationSpeed = 50f; //later used for smoothing rapid turns of the player

    public Vector3 currentMoveDirection, currentLookDirection;
    private Vector3 forward, right, moveVelocity, pointToLook, dashFrom, dashTo;

    private bool isAiming;
    public bool doubleTrue = false;
    public bool isDashing = false;
    private float timeStartDash, currentDashValueTime;
    public float dashTime, dashValue, dashValueTime, maxDashValue;
    public float dashDistance = 7f;

    private GameObject _child;
    Plane groundPlane;
    private RaycastHit rayCastHit, hit;

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

        input.Gameplay.Rotate.performed += rt => GamepadLook(rt.ReadValue<Vector2>());
        input.Gameplay.Look.performed += rt => MouseLook(rt.ReadValue<Vector2>());
        input.Gameplay.Dash.performed += ctx => DashActivator();
        //input.Gameplay.GrenadeThrow.performed += ctx => Aim();
        // input.Gameplay.Sprint.performed += ctx => Sprint();
    }

    void Start()
    {
        //Set up for the movement
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        Cursor.visible = true;
        _child = gameObject.transform.GetChild(0).gameObject;
    }


    private void Update()
    {
        DashValueIncreaser();
        UpdateLookDirection();
        Move();
        DashUpdate();
        // InputDebug();
    }

    void InputDebug()
    {
        if (input.Gameplay.LeftAttack.triggered)
        {
            Debug.Log("Left is triggerd");

            StartCoroutine(Timing());
        }

        if (input.Gameplay.RightAttack.triggered & doubleTrue)
        {
            Debug.Log("both are Triggered");
        }

    }

    #endregion

    IEnumerator Timing()
    {
        doubleTrue = true;
        yield return new WaitForSeconds(1.0f);
        doubleTrue = false;
        yield return null;
    }

    #region Movement

    public void Move()
    {
        Vector2 move = input.Gameplay.Movement.ReadValue<Vector2>();
        Vector3 direction = new Vector3(move.x, 0, move.y);

        moveVelocity = direction * moveSpeed * Time.deltaTime;
        Vector3 horizMovement = right * moveVelocity.x;
        Vector3 vertikMovement = forward * moveVelocity.z;

        transform.position += horizMovement;
        transform.position += vertikMovement;

        currentMoveDirection = horizMovement + vertikMovement;
    }

    public void Aim()
    {
        isAiming = true;
    }


    #endregion

    #region Dash

    public void DashActivator()
    {
        if (dashValue == maxDashValue)
        {
            timeStartDash = Time.time;


            isDashing = true;
            dashFrom = transform.position;
            if (!Physics.Raycast(transform.position, currentMoveDirection, out hit, dashDistance))
            {
                dashTo = transform.position + currentMoveDirection * dashDistance;
            }
            else
            {
                dashTo = hit.point;
            }
        }
    }

    public Vector3 Dash(Vector3 start, Vector3 end, float timeStartDash, float dashTime)
    {
        float timeSinceStarted = Time.time - timeStartDash;

        float percentageComplete = timeSinceStarted / dashTime;
        _child.GetComponent<Animator>().SetBool("Dash", true);


        return Vector3.Lerp(start, end, percentageComplete);
    }

    public void DashUpdate()
    {
        if (isDashing)
        {
            transform.position = Dash(dashFrom, dashTo, timeStartDash, dashTime);
            gameObject.transform.GetChild(3).GetComponent<Collider>().enabled = false;

            if (transform.position == dashTo)
            {
                dashValue = 0;
                isDashing = false;
                gameObject.transform.GetChild(3).GetComponent<Collider>().enabled = true;
                _child.GetComponent<Animator>().SetBool("Dash", false);
                currentDashValueTime = Time.time;
            }
        }
    }

    public void DashValueIncreaser()
    {
        float timeSinceDashEnded = Time.time - currentDashValueTime;

        float perc = timeSinceDashEnded / dashValueTime;

        dashValue = Mathf.Lerp(0, maxDashValue, perc);
    }

    #endregion

    #region Sprint

    public void Sprint()
    {
        StartCoroutine(SprintCoroutine());
    }

    IEnumerator SprintCoroutine()
    {
        moveSpeed *= 3;
        yield return new WaitForSeconds(2);
        moveSpeed /= 3;
    }

    #endregion

    #region Look direction

    public void GamepadLook(Vector2 rotate)
    {
        var lookRot = mainCam.transform.TransformDirection(new Vector3(rotate.x, 0, rotate.y));
        pointToLook = Vector3.ProjectOnPlane(lookRot, Vector3.up);
    }

    public void MouseLook(Vector2 rotate)
    {
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