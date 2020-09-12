using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "AnimClip", menuName = "Animation/Att anim Clip", order = 0)]
public class AttackState : ScriptableObject
{
    public TimelineAsset anim;
    public AnimationClip clip;
    public PlayerMovementSate movementState;
    public bool canAttack;
    public bool canTurn;
    public bool canDamage;
    public float maxRot;
    private AttackState _nextState;
}