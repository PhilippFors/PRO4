using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace _03_Scripts.Entities.Player.PlayerAttackStates
{
    [CreateAssetMenu(fileName = "AttackStates", menuName = "AttackStates")]
    public class AttackSO : ScriptableObject
    {
        public Skills skill;
        public List<AttackSO> nextAttacks;
        public List<State> stateList;
        
    }
}