using System.Collections.Generic;
using _03_Scripts.Entities.Player.PlayerAttackStates;
using UnityEngine;

namespace _03_Scripts.Entities.Player
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "Weapon", order = 0)]
    public class Weapons : ScriptableObject
    {
        public List<AttackSO> attacks;
        public GameObject weapon;
    }
}