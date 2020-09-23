using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "AttackStates", menuName = "Animation/AttackStates")]
public class AttackSO : ScriptableObject
{
    public Skills skill;
    public List<AttackSO> nextAttacks;
    public List<AttackState> stateList;
    public float comboDamageMultiplier;
}