using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachineController : MonoBehaviour
{
    #region "Variables"
    [HideInInspector] public EnemyBody enemyStats => GetComponent<EnemyBody>();
    [HideInInspector] public NavMeshAgent agent => GetComponent<NavMeshAgent>();
    [HideInInspector] public IEnemyActions actions => GetComponent<IEnemyActions>();
    [HideInInspector] public AIManager settings;
    [HideInInspector] public AISteering steering;
    [HideInInspector] public Vector3 offsetTargetPos;
    [HideInInspector] public Transform ObstacleTarget;
    [HideInInspector] public float deltaTime;
    [HideInInspector] public bool avoidDirection;
    [HideInInspector] public bool checkedAmount;

    [SerializeField] public bool aiActive = false, isGrounded = true;

    public Transform RayEmitter;
    public State currentState;
    public State startState;
    public State remainState;
    public Transition[] anyTransitions;

    #endregion

    private void Start()
    {
        steering = new AISteering();
    }

    private void OnEnable()
    {
        aiActive = false;
    }

    public void SetAI(bool active)
    {
        aiActive = active;
    }

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

        steering.IsGrounded(this);
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

    public void SwitchStates(State nextState)
    {
        if (nextState != null && nextState != remainState)
        {
            currentState.StateExit(this);
            currentState = nextState;
            currentState.StateEnter(this);
        }
    }
}
