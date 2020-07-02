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
        public List<AttackSO> attacks;
        public AttackSO currentAttack;
        public State currentState;
        public PlayableDirector director;
        [HideInInspector] public PlayerControls input;
        private float timer = 0;
        private int counter = 0;
        private static PlayerAttack attack => GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
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

        private void Attack(int stateID) {
            if (currentState.canAttack || currentAttack == baseAttack)
            {
                counter = 0;
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

        private void SetState(State state)
        {
            currentState = state;
            timer = 0;
            director.Play(currentState.anim);
            EventSystem.instance.OnSetState(currentState.movementState);

        }
        
        
        public void Update()
        {
            timer += Time.deltaTime;
            if (timer >= currentState.anim.duration && currentState != baseState)
            {
                if ((counter + 1) == currentAttack.stateList.Count)
                {
                    attack.comboCounter = 0;
                    currentState = baseState;
                    currentAttack = baseAttack;
                    counter = 0;
                    EventSystem.instance.OnSetState(PlayerMovmentSate.standard);
                    
                }
                else
                {
                    counter++;
                    SetState(currentAttack.stateList[counter]); 

                }
               
            }
            
        }
    }
}
