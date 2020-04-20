using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachineController : MonoBehaviour
{
    public string playertag = "Player";
    [HideInInspector] public EnemyBaseClass enemystats => GetComponent<EnemyBaseClass>();
    public State currentState;
    public State remainState;
    public State startState;
    public bool aiActive;
    public Transform target { get; private set; }
    public Animator animator;
    public bool isGrounded = true;
    Vector3 velocity;
    LayerMask groundMask => LayerMask.GetMask("Ground");
    
    public float deltaTime;
    public NavMeshAgent agent => GetComponent<NavMeshAgent>();
    public EnemyTestWeapon weapon;

    void Start()
    {

    }

    void Update()
    {   
        deltaTime = Time.deltaTime;
        if (!aiActive){
            agent.isStopped = true;
            return;
        }
            

        if (currentState == null)
            currentState = startState;

        currentState.StateUpdate(this);

        IsGrounded();
    }


    void IsGrounded()
    {
        if (Physics.CheckSphere(transform.position + new Vector3(0, -0.005f, 0),1f, groundMask, QueryTriggerInteraction.Ignore))
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
        else if (GameObject.FindGameObjectWithTag(playertag) != null)
            target = GameObject.FindGameObjectWithTag(playertag).GetComponent<Transform>();
        else
            Debug.LogError("Could not find Player!");
    }
}
