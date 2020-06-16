using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace _03_Scripts.Entities.Player.PlayerAttackStates
{
    [CreateAssetMenu(fileName = "AttackStates", menuName = "AttackStates", order = 0)]
    public class AttackStates : ScriptableObject
    {
        public TimelineAsset anim;
        public TimelineAsset returnAnim;
        public List<AttackStates> nextStates = new List<AttackStates>(2);
        public int comboID = 0; //how far in the combo the attack is
        public Skills skill;
        public float holdTime;
    }
}