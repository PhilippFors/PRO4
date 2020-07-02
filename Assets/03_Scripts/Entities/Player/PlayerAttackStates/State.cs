using UnityEngine;
using UnityEngine.Timeline;

namespace _03_Scripts.Entities.Player.PlayerAttackStates
{
    [CreateAssetMenu(fileName = "States", menuName = "Attacksasdfasdf", order = 0)]
    public class State : ScriptableObject
    {
        public TimelineAsset anim;
        public PlayerMovmentSate movementState;
        public bool canAttack;
        private State _nextState;
    }
}