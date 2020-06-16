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
    [HideInInspector] public float deltaTime;
    [HideInInspector] public float time;
    public float moveSpeed = 5.0f, grenadeMove = 3.0f, standardMoveSpeed = 8.0f, dashValue, dashValueTime, maxDashValue;
    public float dashForce = 1.0f, dashDuration = 0.3f, dashDistance = 7f, drag = 1f, delayTime;

    #endregion

    #region __________other__________
    [HideInInspector] public GameObject dashTarget;
    [HideInInspector] public Rigidbody rb => GetComponent<Rigidbody>();
    [HideInInspector] public LayerMask groundMask => LayerMask.GetMask("Ground");
    [HideInInspector] public LayerMask enemyMask => LayerMask.GetMask("Enemy");
    [HideInInspector] public PlayerControls input;
    public Transform RayEmitter;
    PlayerMovmentSate currentState;
    PlayerMovementController standardMovement;
    DashMovementController dashController;
    GrenadeMovementController grenadeController;
    private AttackState attackController;

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

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + currentMoveDirection.normalized * moveSpeed * Time.fixedDeltaTime);
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
    void ResetMoveSpeed(){
        moveSpeed = standardMoveSpeed;
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
        moveSpeed = grenadeMove;
    }

}
