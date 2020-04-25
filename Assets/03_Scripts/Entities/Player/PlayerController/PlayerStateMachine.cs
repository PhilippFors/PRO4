using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerMovmentSate
{
    standard,
    dash,
    grenade,
    attack
}

public class PlayerStateMachine : MonoBehaviour
{

    #region __________Vector 3/2__________

    [HideInInspector] public Vector3 currentMoveDirection, currentLookDirection;
    public Vector2 move;
    public Vector2 gamepadRotate;
    public Vector2 mouseLook;
    #endregion

    #region __________bool__________

    [HideInInspector] public bool isAiming, mouseused, gamepadused;
    public bool isGrounded = true;
    public bool checkEnemy = false;

    #endregion

    #region __________float__________

    // [SerializeField] private float rotationSpeed = 50f; //later used for smoothing rapid turns of the player
    public float moveSpeed = 5.0f;

    public float dashValue, dashValueTime, maxDashValue;
    public float dashForce = 1.0f, dashDuration = 0.3f, dashDistance = 7f, drag = 1f, delayTime, frametime = 0.0f, delayCountdown;

    #endregion

    #region __________other__________
    [HideInInspector] public GameObject dashTarget;
    [HideInInspector] public Rigidbody rb => GetComponent<Rigidbody>();
    [HideInInspector] public LayerMask groundMask => LayerMask.GetMask("Ground");
    [HideInInspector] public LayerMask enemyMask => LayerMask.GetMask("Enemy");
    [HideInInspector] public PlayerControls input;

    public Transform RayEmitter;
    PlayerMovmentSate currentState;
    PlayerMovementController standardMovement => GetComponent<PlayerMovementController>();
    DashMovementController dashController => GetComponent<DashMovementController>();
    #endregion

    private void Awake()
    {
        input = new PlayerControls();
        input.Gameplay.Dash.performed += ctx => StartDash();
    }
    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }
    private void Start()
    {
        SetState(PlayerMovmentSate.standard);
        frametime = dashDuration;
        delayCountdown = delayTime;
    }
    void Update()
    {
        GetInputValues();
        switch (currentState)
        {
            case PlayerMovmentSate.standard:
                standardMovement.Tick(this);
                break;
            case PlayerMovmentSate.dash:
                dashController.Tick(this);
                break;
        }
        dashController.DashCooldown(this);
    }

    void GetInputValues()
    {
        move = input.Gameplay.Movement.ReadValue<Vector2>();
        gamepadRotate = input.Gameplay.Rotate.ReadValue<Vector2>();
        mouseLook = input.Gameplay.Look.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + currentMoveDirection.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    public void SetState(PlayerMovmentSate state)
    {
        currentState = state;
    }

    public void StartDash()
    {
        dashController.DashInit(this);
        SetState(PlayerMovmentSate.dash);
    }
}
