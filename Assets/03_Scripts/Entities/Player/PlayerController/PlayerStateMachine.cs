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

    #region __________Vector 2&3__________

    [HideInInspector] public Vector3 currentMoveDirection, currentLookDirection;
    [HideInInspector] public Vector3 forward, right, pointToLook;
    [HideInInspector] public Vector2 move;
    [HideInInspector] public Vector2 gamepadRotate;
    [HideInInspector] public Vector2 mouseLook;
    public Vector3 velocity = Vector3.zero;

    #endregion

    #region __________bool__________

    [HideInInspector] public bool isAiming, mouseused, gamepadused;
    [HideInInspector] public bool isGrounded = true;
    [HideInInspector] public bool checkEnemy = false;
    public bool isMoving = false;

    #endregion

    #region __________float__________

    // [SerializeField] private float rotationSpeed = 50f; //later used for smoothing rapid turns of the player
    [HideInInspector] public float deltaTime;
    [HideInInspector] public float time;

    public float currentMoveSpeed = 5.0f, grenadeMoveSpeed = 3.0f, standardMoveSpeed = 8.0f, dashValue, dashValueTime, maxDashValue;
    public float dashForce = 1.0f, dashDuration = 0.3f, dashDistance = 7f, drag = 1f, delayTime;

    #endregion

    #region __________other__________
    [HideInInspector] public GameObject dashTarget;
    [HideInInspector] public Rigidbody rb => GetComponent<Rigidbody>();
    [HideInInspector] public LayerMask groundMask => LayerMask.GetMask("Ground");
    [HideInInspector] public LayerMask enemyMask => LayerMask.GetMask("Enemy");
    [HideInInspector] public PlayerControls input;
    public Transform RayEmitter;
    public PlayerMovmentSate currentState;
    PlayerMovementController standardMovement;
    DashMovementController dashController;
    GrenadeMovementController grenadeController;

    private AttackState attackController;
    GroundChecker groundChecker => GetComponent<GroundChecker>();
    public PlayerAttack target => GetComponent<PlayerAttack>();

    #endregion

    private void Awake()
    {
        input = new PlayerControls();
        standardMovement = new PlayerMovementController(this);
        dashController = new DashMovementController(this);
        attackController = new AttackState(this);
        grenadeController = new GrenadeMovementController(this);

        input.Gameplay.Dash.performed += ctx => SetState(PlayerMovmentSate.dash);
        input.Gameplay.Movement.performed += ctx => IsMoving();
        input.Gameplay.Movement.canceled += ctx => IsNotMoving();
    }

    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
        EventSystem.instance.SetState -= SetState;
       

    }

    private void Start()
    {
        SetState(PlayerMovmentSate.standard);
        EventSystem.instance.SetState += SetState;
       
    }

    void Update()
    {
        time = Time.time;
        deltaTime = Time.deltaTime;
        IsGrounded();
        GetInputValues();
        switch (currentState)
        {
            case PlayerMovmentSate.standard:
                standardMovement.Tick(this);
                break;
            case PlayerMovmentSate.dash:
                dashController.Tick(this);
                break;
            case PlayerMovmentSate.grenade:
                grenadeController.Tick(this);
                break;
            case PlayerMovmentSate.attack:
                attackController.Tick(this);
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

    void IsMoving()
    {
        isMoving = true;
    }

    void IsNotMoving()
    {
        isMoving = false;
    }

    private void FixedUpdate()
    {
        CheckSlope();

        rb.MovePosition(transform.position + Vector3.Normalize(currentMoveDirection + velocity) * currentMoveSpeed * Time.fixedDeltaTime);
    }

    public void IsGrounded()
    {
        if (Physics.CheckSphere(transform.position + new Vector3(0, 1f, 0), 1.01f, groundMask, QueryTriggerInteraction.Ignore))
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

    void CheckSlope()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.3f, groundMask))
        {
            velocity = new Vector3(0, -hit.distance, 0);
        }
        if (!isMoving)
            velocity = Vector3.zero;
    }
    public void SetState(PlayerMovmentSate state)
    {
        switch (state)
        {
            case PlayerMovmentSate.attack:
                Attack();
                break;
            case PlayerMovmentSate.dash:
                StartDash();
                break;
            case PlayerMovmentSate.standard:
                ResetMoveSpeed();
                break;
            case PlayerMovmentSate.grenade:
                GrenadeMoveSpeed();
                break;
        }
        currentState = state;
    }
    void ResetMoveSpeed()
    {
        currentMoveSpeed = standardMoveSpeed;
    }

    public void StartDash()
    {
        dashController.DashInit(this);
    }

    public void Attack()
    {
        attackController.StopMovement(this);
    }

    void GrenadeMoveSpeed()
    {
        currentMoveSpeed = grenadeMoveSpeed;
    }

}
