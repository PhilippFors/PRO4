using System.Collections.Generic;
using _03_Scripts.Entities.Player.PlayerAttackStates;
using UnityEngine;

namespace _03_Scripts.Entities.Player
{
    public class Weapons : MonoBehaviour
    {
        public List<AttackSO> attacks = new List<AttackSO>();
        public int damage;
    }
}