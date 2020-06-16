using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.PlayerLoop;

namespace _03_Scripts.Entities.Player.PlayerAttackStates
{
    public class AttackStateMachine : MonoBehaviour
    {
        public List<AttackStates> attackStates;
        public AttackStates currentState;
        public PlayableDirector director;
        [HideInInspector] public PlayerControls input;
        private bool returnPlayed = false;
        public bool canAttack = true;
        public float attackTime = 2;
        private static PlayerAttack attack => GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();

        private void Awake()
        {
            input = new PlayerControls();

            input.Gameplay.LeftAttack.performed += ctx => SetState(0);
            input.Gameplay.RightAttack.performed += ctx => SetState(1);
            director = gameObject.GetComponent<PlayableDirector>();
        }

        private void OnEnable()
        {
            input.Enable();
            director.stopped += Comboing;
        }

        private void OnDisable()
        {
            input.Disable();
            director.stopped -= Comboing;
        }

        private void Start()
        {
            currentState = attackStates[0];
        }

        private void SetState(int stateID)
        {
            if (stateID != null && canAttack)
            {
                EventSystem.instance.OnSetState(PlayerMovmentSate.attack);
                canAttack = false;
                currentState = currentState.nextStates[stateID];
                director.Play(currentState.anim);
                StopCoroutine("Timer");
                if (attack.skills.Contains(currentState.skill) && attack.comboCounter >= 4)
                {
                    int a = attack.skills.IndexOf(currentState.skill);
                    attack.skills[a].current += 2;
                    attack.comboCounter = 0;
                }

                returnPlayed = false;
            }
        }

        public void Update()
        {
            if (director.state != PlayState.Playing)
            {
                canAttack = true;
            }
        }


        private void ReturnAnim()
        {
            director.Play(currentState.returnAnim);
            currentState = attackStates[0];
            returnPlayed = true;
            attack.comboCounter = 0;


            EventSystem.instance.OnSetState(PlayerMovmentSate.standard);
        }

        private void Comboing(PlayableDirector a)
        {
            if (director == a && !returnPlayed)
            {
                canAttack = true;
                StartCoroutine("Timer");
            }
        }

        IEnumerator Timer()
        {
            yield return new WaitForSecondsRealtime(attackTime);
            canAttack = false;
            ReturnAnim();
            yield return null;
        }
    }
}