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
    public float animTimer = 0; // a timer to check if animation has finished playing
    private int stateCounter = 0; // counts the current states of an attack
    private static PlayerAttack playerAttack => GetComponent<PlayerAttack>();
    AnimationController animCon => GetComponent<AnimationController>();
    PlayerStateMachine _stateMachine;
    public AttackSO baseAttack; //an attack which has no states and only holds the next attack
    public AttackState baseState; // an empty state that does nothing
    PlayableGraph playableGraph;
    public Animator animator => GetComponent<Animator>(); //animator of the character model
    public AnimationClip lastClip;

    private void Awake()
    {
        _stateMachine = GetComponent<PlayerStateMachine>();
        StartCoroutine(InitInput());
    }

    IEnumerator InitInput()
    {
        yield return new WaitForEndOfFrame();
        _stateMachine.input.Gameplay.LeftAttack.performed += ctx => Attack(0);
        _stateMachine.input.Gameplay.RightAttack.performed += ctx => Attack(1);
    }

    private void OnDisable()
    {

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
        if (_stateMachine.currentState == PlayerMovementSate.standard || _stateMachine.currentState == PlayerMovementSate.comboWait && (currentState.canAttack || currentAttack != null))
        {
            lastClip = currentState.clip;
            stateCounter = 0; //sets the counter back to zero because a new attack begins
            currentAttack =
                currentAttack.nextAttacks[stateID]; //the new currentattack based on which attack button got pressed
            SetState(currentAttack.stateList[0]); //sets first (attack) state of the attack
            playerAttack.currentWeapon.gameObject.GetComponent<Collider>().enabled = true;

            //checks if the combo is reached and increases skillmeter and sets combo back to 0
            if (playerAttack.skills.Contains(currentAttack.skill) && playerAttack.comboCounter >= currentAttack.skill.comboCounter)
            {
                int a = playerAttack.skills.IndexOf(currentAttack.skill);
                IncreaseOneSkill(2, playerAttack.skills[a]);
                // playerAttack.skills[a].current += 2;
                playerAttack.comboCounter = 0;
            }
        }
    }
    public void IncreaseOneSkill(float amount, Skills skill)
    {
        if (skill.current < skill.max)
        {
            if (skill.current + amount > skill.max)
            {
                skill.current = skill.max;
            }
            else
            {
                skill.current += amount;

            }
        }
    }
    public void IncreaseAllSkills(float amount, out bool increased)
    {
        increased = true;
        foreach (Skills skill in playerAttack.skills)
        {
            if (skill.current < skill.max)
            {
                if (skill.current + amount > skill.max)
                {
                    skill.current = skill.max;
                    increased = true;
                }
                else
                {
                    skill.current += amount;
                    increased = true;
                }
            }
            else
            {
                increased = false;
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
        animCon.AttackAnimation(lastClip, currentState.clip);
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
                lastClip = currentState.clip;
                SetState(currentAttack.stateList[stateCounter]);
                playerAttack.currentWeapon.gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }

    public void SetBase(AttackSO state = null)
    {
        playerAttack.comboCounter = 0;
        lastClip = currentState.clip;
        if (state != null)
        {
            currentAttack = state;
        }
        else
        {
            currentAttack = playerAttack.currentWeapon.baseAttack;
            EventSystem.instance.OnSetState(PlayerMovementSate.standard);
        }
        currentState = baseState;
        //sets movementState to the movementstate of the currentstate
        stateCounter = 0;
    }
}
