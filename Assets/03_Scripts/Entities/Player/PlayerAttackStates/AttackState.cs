using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "States", menuName = "Attacksasdfasdf", order = 0)]
public class AttackState : ScriptableObject
{
    public TimelineAsset anim;
    public AnimationClip clip;
    public PlayerMovmentSate movementState;
    public bool canAttack;
    public bool canTurn;
    public bool canDamage;
    public float maxRot;
    private AttackState _nextState;
}