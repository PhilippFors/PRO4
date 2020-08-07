﻿using UnityEngine;
using UnityEngine.Timeline;

namespace _03_Scripts.Entities.Player.PlayerAttackStates
{
    [CreateAssetMenu(fileName = "States", menuName = "Attacksasdfasdf", order = 0)]
    public class State : ScriptableObject
    {
        public TimelineAsset anim;
        public AnimationClip clip;
        public PlayerMovementSate movementState;
        public bool canAttack;
        public bool canTurn;
        public bool canDamage;
        public float maxRot;
        private State _nextState;
    }
}