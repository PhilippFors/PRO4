using System.Collections;
using System.Collections.Generic;
using FMOD;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using Debug = UnityEngine.Debug;

public enum PlayerMovementSate
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
    [HideInInspector] public Vector3 forward, right, pointToLook, currentLook;
    [HideInInspector] public Vector2 move;
    [HideInInspector] public Vector2 gamepadRotate;
    [HideInInspector] public Vector2 mouseLook;
    public Vector3 velocity = Vector3.zero;
    public Vector3 gravity = Vector3.zero;

    #endregion

    #region __________bool__________

    [HideInInspector] public bool isAiming, mouseused, gamepadused;
    public bool isGrounded = false;
    [HideInInspector] public bool checkEnemy = false;
    public bool isMoving = false;

    #endregion

    #region __________float__________

    // [SerializeField] private float rotationSpeed = 50f; //later used for smoothing rapid turns of the player
    [HideInInspector] public float deltaTime;
    [HideInInspector] public float time;

    public float currentMoveSpeed = 5.0f, grenadeMoveSpeed = 3.0f, standardMoveSpeed = 8.0f, dashValue, dashValueTime, maxDashValue;
    public float dashForce = 1.0f, dashDuration = 0.3f, dashDistance = 7f, drag = 1f, delayTime;
    private float afterDrag = 10;

    #endregion

    #region __________other__________
    [HideInInspector] public GameObject dashTarget;
    [HideInInspector] public Rigidbody rb => GetComponent<Rigidbody>();
    [HideInInspector] public LayerMask groundMask => LayerMask.GetMask("Ground");
    [HideInInspector] public LayerMask enemyMask => LayerMask.GetMask("Enemy");
    [HideInInspector] public PlayerControls input;
    public Transform RayEmitter;
    public PlayerMovementSate currentState;
    PlayerMovementController standardMovement;
    DashMovementController dashController;
    GrenadeMovementController grenadeController;

    private AttackState attackController;
    GroundChecker groundChecker => GetComponent<GroundChecker>();
    public PlayerAttack target => GetComponent<PlayerAttack>();

    public AnimationClip clip;

    PlayableGraph playableGraph;
    public CharacterController characterController => GetComponent<CharacterController>();
    [SerializeField] private StatTemplate playerTemplate;

    #endregion

    private void Awake()
    {
        input = new PlayerControls();
        standardMovement = new PlayerMovementController(this);
        dashController = new DashMovementController(this);
        attackController = new AttackState(this);
        grenadeController = new GrenadeMovementController(this);

        input.Gameplay.Dash.performed += ctx => SetState(PlayerMovementSate.dash);
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
        playableGraph.Destroy();

    }

    private void Start()
    {
        SetState(PlayerMovementSate.standard);
        EventSystem.instance.SetState += SetState;

        playableGraph = PlayableGraph.Create();

        playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

        var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Run", GetComponentInChildren<Animator>());

        // Wrap the clip in a playable

        var clipPlayable = AnimationClipPlayable.Create(playableGraph, clip);

        // Connect the Playable to an output

        playableOutput.SetSourcePlayable(clipPlayable);

        foreach (FloatReference f in playerTemplate.statList)
        {
            StatVariable s = (StatVariable)f.Variable;
            if (s.statName.ToString().Equals("Speed"))
            {
                standardMoveSpeed = s.Value;
            }
        }
        grenadeMoveSpeed = standardMoveSpeed / 2;
    }

    void Update()
    {
        time = Time.time;
        deltaTime = Time.deltaTime;
        IsGrounded();
        GetInputValues();
        switch (currentState)
        {
            case PlayerMovementSate.standard:
                // CheckSlope();
                standardMovement.Tick(this);
                break;
            case PlayerMovementSate.dash:
                dashController.Tick(this);
                break;
            case PlayerMovementSate.grenade:
                grenadeController.Tick(this);
                break;
            case PlayerMovementSate.attack:
                attackController.Tick(this);
                break;
        }
        Move();
        dashController.DashCooldown(this);
    }

    void GetInputValues()
    {
        move = input.Gameplay.Movement.ReadValue<Vector2>();
        gamepadRotate = input.Gameplay.Rotate.ReadValue<Vector2>();
        mouseLook = input.Gameplay.Look.ReadValue<Vector2>();
    }

    void Move()
    {
        velocity.y = 0;
        characterController.Move(((Vector3.Normalize(currentMoveDirection) + velocity) * currentMoveSpeed) * Time.deltaTime);
    }

    void IsMoving()
    {
        isMoving = true;
        if (currentState == PlayerMovementSate.standard)
        {
            PlayAnim();
        }
    }

    void IsNotMoving()
    {
        isMoving = false;
        playableGraph.Stop();
        // Debug.Log(isMoving);
    }

    // private void FixedUpdate()
    // {


    //     // rb.MovePosition(transform.position + Vector3.Normalize(currentMoveDirection + velocity) * currentMoveSpeed * Time.fixedDeltaTime);
    // }

    public void IsGrounded()
    {
        if (Physics.CheckSphere(transform.position + new Vector3(0, 1f, 0), 1.01f, groundMask, QueryTriggerInteraction.Ignore))
        {
            // drag = 0;
            isGrounded = true;
            
        }
        else
        {
            // drag = afterDrag;
            isGrounded = false;
            characterController.Move(Physics.gravity*Time.deltaTime);
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
    public void SetState(PlayerMovementSate state)
    {
        velocity = Vector3.zero;
        switch (state)
        {
            case PlayerMovementSate.attack:
                Attack();
                break;
            case PlayerMovementSate.dash:
                StartDash();
                break;
            case PlayerMovementSate.standard:
                ResetMoveSpeed();
                break;
            case PlayerMovementSate.grenade:
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

    void PlayAnim()
    {

        // Plays the Graph.

        playableGraph.Play();
    }

}
