using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachineController : MonoBehaviour
{
    [HideInInspector] public EnemyBody enemyStats => GetComponent<EnemyBody>();
    [HideInInspector] public NavMeshAgent agent => GetComponent<NavMeshAgent>();
    [HideInInspector] public IEnemyActions actions => GetComponent<IEnemyActions>();
    [HideInInspector] public LayerMask groundMask => LayerMask.GetMask("Ground");
    [HideInInspector] public LayerMask enemyMask => LayerMask.GetMask("Enemy");
    [HideInInspector] public AIManager settings;
    [SerializeField] private bool aiActive = false, isGrounded = true;
    [HideInInspector] public Transform target;
    Vector3 velocity;
    [HideInInspector] public Vector3 currentPos;
    [HideInInspector] public float deltaTime;
    public Transform RayEmitter;
    public State currentState;
    public State startState;
    public State remainState;
    public Transition[] anyTransitions;
    public Vector3 cachedRayemitterrot;
    public Vector3 seperationHeading;
    public bool avoidDirection;
    public bool checkedAmount;
    public Vector3 offsetTargetPos;
    void Update()
    {
        deltaTime = Time.deltaTime;
        if (!aiActive)
        {
            agent.isStopped = true;
            return;
        }

        if (currentState == null)
            currentState = startState;

        currentState.StateUpdate(this);

        CheckAnyTransitions(this);

        IsGrounded();
    }

    private void CheckAnyTransitions(StateMachineController controller)
    {
        if (anyTransitions.Length == 0)
            return;

        foreach (Transition transition in anyTransitions)
        {
            if (transition.decision.Execute(controller))
            {
                controller.SwitchStates(transition.trueState);
            }
            else
            {
                controller.SwitchStates(transition.falseState);
            }
        }
    }

    private void OnEnable()
    {
        aiActive = false;
    }
    public void SetAI(bool active)
    {
        aiActive = active;
    }

    void IsGrounded()
    {
        if (Physics.CheckSphere(transform.position + new Vector3(0, 0.9f, 0), 1f, groundMask, QueryTriggerInteraction.Ignore))
        {
            isGrounded = true;
            velocity = Vector3.zero;
        }
        else
        {
            isGrounded = false;
            velocity.y = Physics.gravity.y * Time.deltaTime;
        }

        transform.position += velocity;
    }

    public void SwitchStates(State nextState)
    {
        if (nextState != null && nextState != remainState)
        {
            currentState.StateExit(this);
            currentState = nextState;
            currentState.StateEnter(this);
        }
    }

    public void FindPlayer()
    {
        if (FindObjectOfType<PlayerBody>() != null)
            target = FindObjectOfType<PlayerBody>().GetComponent<Transform>();
        else if (GameObject.FindGameObjectWithTag(settings.playertag) != null)
            target = GameObject.FindGameObjectWithTag(settings.playertag).GetComponent<Transform>();
        else
            Debug.LogError("Could not find Player!");
    }
}
