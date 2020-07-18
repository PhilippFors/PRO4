using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.PlayerLoop;

public enum States
{
    attackState,
    waitState,
    returnState,
    nonattack
}


namespace _03_Scripts.Entities.Player.PlayerAttackStates
{
    public class AttackStateMachine : MonoBehaviour
    {
        public AttackSO currentAttack;
        public State currentState;
        public PlayableDirector director;
        [HideInInspector] public PlayerControls input;
        private float animTimer = 0;
        private int stateCounter = 0;
        public float maxRot = 45;
        private static PlayerAttack attack => GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        private static PlayerMovmentSate movementState => GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerStateMachine>().currentState;
        public AttackSO baseAttack;
        public State baseState;

        private void Awake()
        {
            input = new PlayerControls();

            input.Gameplay.LeftAttack.performed += ctx => Attack(0);
            input.Gameplay.RightAttack.performed += ctx => Attack(1);
            director = gameObject.GetComponent<PlayableDirector>();
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
            currentAttack = baseAttack;
            currentState = baseState;
        }

        private void Attack(int stateID)
        {
           
            if (movementState.Equals(PlayerMovmentSate.standard))
            {
                if (currentState.canAttack || currentAttack == baseAttack)
                {
                    stateCounter = 0;
                    currentAttack = currentAttack.nextAttacks[stateID];
                    SetState(currentAttack.stateList[0]);
                
                
                    if (attack.skills.Contains(currentAttack.skill) && attack.comboCounter >= 4)
                    {
                        int a = attack.skills.IndexOf(currentAttack.skill);
                        attack.skills[a].current += 2;
                        attack.comboCounter = 0;
                    }
                }
            }
          
        }

        private void SetState(State state)
        {
            currentState = state;
            animTimer = 0;
            /*if (state.maxRot != null)
            {
                maxRot = state.maxRot;
            }*/
            director.Play(currentState.anim);
            EventSystem.instance.OnSetState(currentState.movementState);

        }
        
        
        public void Update()
        {
            animTimer += Time.deltaTime;
            if (animTimer >= currentState.anim.duration && currentState != baseState)
            {
                if ((stateCounter + 1) == currentAttack.stateList.Count || currentAttack.stateList.Count.Equals(0))
                {
                    attack.comboCounter = 0;
                    currentState = baseState;
                    currentAttack = baseAttack;
                    stateCounter = 0;
                    //maxRot = 0;

                }
                else
                {
                    stateCounter++;
                    SetState(currentAttack.stateList[stateCounter]); 

                }
               
            }
            
        }
    }
}
