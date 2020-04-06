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

    Vector3 forward, right;
    Vector3 moveVelocity;
    Vector3 pointToLook;
    Vector2 move = Vector3.zero;

    public bool doubleTrue = false;
    public float dashDistance = 7f;

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
        input.Gameplay.Dash.performed += ctx => Dash();
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
    }


    private void Update()
    {
        // if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        // {
        UpdateLookDirection();
        Move();
        // InputDebug();
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

    private bool CanMove(Vector3 dir, float distance)
    {
        return !Physics.Raycast(transform.position, dir, out rayCastHit, distance); ;
    }

    public void Dash()
    {
        if (CanMove(currentDirection.normalized, dashDistance))
        {
            transform.position += currentDirection.normalized * dashDistance;
        }
        else
        {
            transform.position = rayCastHit.point;
        }
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