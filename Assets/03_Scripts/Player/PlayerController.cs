using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private Camera mainCam;

    Vector3 forward, right;
    Vector3 moveVelocity;
    Vector3 pointToLook;
    public Vector3 currentDirection;

    Plane groundPlane;

    PlayerControls input;

    Vector2 move = Vector3.zero;
    Vector2 rotate = Vector3.zero;
    private RaycastHit rayCastHit;

    #region PlayerController controls enable/disable

    private void OnEnable()
    {
        input.Gameplay.Enable();

    }

    private void OnDisable()
    {
        input.Gameplay.Disable();
    }
    #endregion

    #region Start/Awake
    private void Awake()
    {

        input = new PlayerControls();
        //Gamepad
        input.Gameplay.Movement.performed += mv => move = mv.ReadValue<Vector2>();
        input.Gameplay.Movement.canceled += mv => move = Vector2.zero;

        input.Gameplay.Rotate.performed += rt => Rotate(rt.ReadValue<Vector2>());
        input.Gameplay.Look.performed += rt => Look(rt.ReadValue<Vector2>());

        //input.Gameplay.Rotate.canceled += rt => rotate = Vector2.zero;

        input.Gameplay.Dash.performed += ctx => Dash();
    }

    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        Cursor.visible = true;
    }
    #endregion

    private void Update()
    {
        // if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        // {
        Move();

        //  }
    }

    public void Move()
    {
        Vector3 direction = new Vector3(move.x, 0, move.y);
        moveVelocity.y = 0;
        moveVelocity = direction * moveSpeed * Time.deltaTime;
        Vector3 horizMovement = right * moveVelocity.x;
        Vector3 vertikMovement = forward * moveVelocity.z;
        currentDirection = horizMovement + vertikMovement;
        transform.position += horizMovement;
        transform.position += vertikMovement;
    }

    private bool CanMove(Vector3 dir, float distance)
    {
        return !Physics.Raycast(transform.position, dir, out rayCastHit, distance); ;
    }

    public void Dash()
    {

        float dashDistance = 7f;
        if (CanMove(currentDirection.normalized, dashDistance))
        {
            transform.position += currentDirection.normalized * dashDistance;
        }
        else
        {
            transform.position = rayCastHit.point;
        }

    }

    public void Rotate(Vector2 rotate)
    {

        Vector3 input = new Vector3(rotate.x, 0, rotate.y);
        var lookRot = mainCam.transform.TransformDirection(input);
        pointToLook = Vector3.ProjectOnPlane(lookRot, Vector3.up);
        pointToLook.y = 0;
        if (pointToLook != Vector3.zero)
        {
            Quaternion newRot = Quaternion.LookRotation(pointToLook);
            transform.rotation = newRot;
        }

    }

    public void Look(Vector2 rotate)
    {

        groundPlane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));
        Ray cameraRay = mainCam.ScreenPointToRay(rotate);
        float rayLength;
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 rayPoint = cameraRay.GetPoint(rayLength);
            pointToLook = rayPoint - transform.position;
        }
        pointToLook.y = 0;
        if (pointToLook != Vector3.zero)
        {
            Quaternion newRot = Quaternion.LookRotation(pointToLook);
            transform.rotation = newRot;
        }


        //Debug.Log("PointToLook" + pointToLook);
        //Debug.Log("Rotate" + rotate);

    }

}