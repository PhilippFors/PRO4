using System;
using System.Collections;
using System.Collections.Generic;
using FMOD;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.PlayerLoop;
using Debug = UnityEngine.Debug;

public enum AttackStates
{
    attackState,
    waitState,
    returnState,
    nonattack
}

public class AttackStateMachine : MonoBehaviour
{
    public AttackSO currentAttack; //the current attack
    public AttackState currentState; //in which state (attack, wait or return) of the attack the statemachine currently is
    [HideInInspector] public PlayerControls input;
    private float animTimer = 0; // a timer to check if animation has finished playing
    private int stateCounter = 0; // counts the current states of an attack
    private static PlayerAttack playerAttack => GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
    AnimationController animCon => GameObject.FindGameObjectWithTag("Player").GetComponent<AnimationController>();
    PlayerStateMachine _stateMachine => GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>();

    public AttackSO baseAttack; //an attack which has no states and only holds the next attack
    public AttackState baseState; // an empty state that does nothing
    PlayableGraph playableGraph;
    public Animator child => GetComponentInChildren<Animator>(); //animator of the character model

    private void Awake()
    {
        input = new PlayerControls();

        input.Gameplay.LeftAttack.performed += ctx => Attack(0);
        input.Gameplay.RightAttack.performed += ctx => Attack(1);
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
        playableGraph.Destroy();
    }

    private void Start()
    {
        currentAttack = playerAttack.currentWeapon.baseAttack; //current attack is based on the current weapon
        currentState = baseState;

        GraphVisualizerClient.Show(playableGraph);
    }

    private void Attack(int stateID)
    {
        // tests if the player should be able to attack (which means he is either in the baseattack or a wait state)
        if (_stateMachine.currentState == PlayerMovementSate.standard && (currentState.canAttack || currentAttack == baseAttack))
        {
            stateCounter = 0; //sets the counter back to zero because a new attack begins
            currentAttack =
                currentAttack.nextAttacks[stateID]; //the new currentattack based on which attack button got pressed
            SetState(currentAttack.stateList[0]); //sets first (attack) state of the attack
            playerAttack.currentWeapon.gameObject.GetComponent<Collider>().enabled = true;

            //checks if the combo is reached and increases skillmeter and sets combo back to 0
            if (playerAttack.skills.Contains(currentAttack.skill) && playerAttack.comboCounter >= currentAttack.skill.comboCounter)
            {
                int a = playerAttack.skills.IndexOf(currentAttack.skill);
                playerAttack.skills[a].current += 2;
                playerAttack.comboCounter = 0;
            }
        }
    }

    private void SetState(AttackState state)
    {
       
        //sets currentstate and resets animation timer
        currentState = state;
        animTimer = 0;

        //plays the animation of the currentstate
        //AnimationPlayableUtilities.PlayClip(child, currentState.clip, out playableGraph);
        animCon.AttackDisconnecter();
        animCon.AttackAnimation(currentState.clip);
        EventSystem.instance.OnSetState(currentState.movementState); //sets movementState to the movementstate of the currentstate
        
    }


    public void Update()
    {
        //checks if animation is finished playing (and an animation was playing)
        animTimer += Time.deltaTime;
        //if (animTimer >= currentState.anim.duration && currentState != baseState)
        if (currentState != baseState && animTimer >= currentState.clip.averageDuration)
        {
            //checks if we are already in the returnstate and resets everything to start, else changes to the next state
            if ((stateCounter + 1) == currentAttack.stateList.Count || currentAttack.stateList.Count.Equals(0))
            {
                animCon.AttackDisconnecter();
                animCon.MoveStarter();
               
                SetBase();
                //maxRot = 0;
            }
            else
            {
                stateCounter++;
                SetState(currentAttack.stateList[stateCounter]);
                playerAttack.currentWeapon.gameObject.GetComponent<Collider>().enabled = false;
                
            }
        }
    }

    public void SetBase()
    {
        playerAttack.comboCounter = 0;
                
        currentAttack = playerAttack.currentWeapon.baseAttack;
        currentState = baseState;
        EventSystem.instance.OnSetState(currentState.movementState); //sets movementState to the movementstate of the currentstate
        stateCounter = 0;
    }
}