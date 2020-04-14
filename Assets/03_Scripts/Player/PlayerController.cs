using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 50f; //later used for smoothing rapid turns of the player
    [SerializeField] private Camera mainCam;
    public Vector3 currentDirection;


    RaycastHit hit;


    Vector3 forward, right;
    Vector3 moveVelocity;
    Vector3 pointToLook;
    Vector2 move = Vector3.zero;

    private bool isAiming;

    private Vector3 dashFrom;
    private Vector3 dashTo;
    public bool isDashing = false;
    private float timeStartDash;
    public float dashTime;
    public float dashValue;
    public float dashValueTime;
    public float maxDashValue;
    private float currentDashValueTime;
    private GameObject _child;

    public bool doubleTrue = false;
    public float dashDistance = 500f;

    Plane groundPlane;

    PlayerControls input;

    private RaycastHit rayCastHit;

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
        input.Gameplay.GrenadeThrow.performed += ctx => Aim();
        // input.Gameplay.Sprint.performed += ctx => Sprint();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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
        // if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        // {
        DashValueIncreaser();
        UpdateLookDirection();
        Move();
        DashUpdate();
        InputDebug();
        //  }
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
        move = input.Gameplay.Movement.ReadValue<Vector2>();
        //Debug.Log(move);
        Vector3 direction = new Vector3(move.x, 0, move.y);
        moveVelocity = direction * moveSpeed * Time.deltaTime;
        moveVelocity.y = 0;
        Vector3 horizMovement = right * moveVelocity.x;
        Vector3 vertikMovement = forward * moveVelocity.z;
        transform.position += horizMovement;
        transform.position += vertikMovement;

        currentDirection = horizMovement + vertikMovement;
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
            if (!Physics.Raycast(transform.position, currentDirection, out hit, dashDistance))
            {
                dashTo = transform.position + currentDirection * dashDistance;
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
            gameObject.transform.GetChild(4).GetComponent<Collider>().enabled = false;

            if (transform.position == dashTo)
            {
                dashValue = 0;
                isDashing = false;
                gameObject.transform.GetChild(4).GetComponent<Collider>().enabled = true;
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
        Vector3 input = new Vector3(rotate.x, 0, rotate.y);
        var lookRot = mainCam.transform.TransformDirection(input);
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
            //Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * rotationSpeed);
        }
    }

    #endregion
}