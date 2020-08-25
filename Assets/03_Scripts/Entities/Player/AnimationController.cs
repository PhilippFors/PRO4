using System;
using System.Collections.Generic;
using FMOD;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;
using Debug = UnityEngine.Debug;


public class AnimationController : MonoBehaviour
{
    [HideInInspector] public PlayerControls input;
    [HideInInspector] public Vector2 move;
    [HideInInspector] public Vector2 gamepadRotate;
    private PlayerStateMachine controller => gameObject.GetComponent<PlayerStateMachine>();
    private AttackStateMachine attack => gameObject.GetComponent<AttackStateMachine>();
    private Animator child => gameObject.GetComponentInChildren<Animator>();

    public AnimationClip forward, backward, right, left, idle, dash;

    public AnimationClipPlayable runPlayableClip,
        backPlayableClip,
        leftPlayableClip,
        rightPlayableClip,
        idlePlayableClip,
        dashPlayableClip;

    public AnimationClipPlayable attackPlayableClip;
    

    public float weightX;
    public float weightY;

    PlayableGraph playableGraph;

    AnimationMixerPlayable movementBlendPlayable;
    AnimationMixerPlayable mixerPlayable;

    private Vector3 foo;
    private Vector3 bla;
    private Vector3 pah;
    private void Awake()
    {
        input = new PlayerControls();
    }

    void Start()

    {
        // Creates the graph, the mixer and binds them to the Animator.

        playableGraph = PlayableGraph.Create("CharacterAnims");

        var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", child);
        Debug.Log(GetComponentInChildren<Animator>());

        movementBlendPlayable = AnimationMixerPlayable.Create(playableGraph, 5);
        mixerPlayable = AnimationMixerPlayable.Create(playableGraph, 3);

        playableOutput.SetSourcePlayable(mixerPlayable);

        // Creates AnimationClipPlayable and connects them to the mixer.

        runPlayableClip = AnimationClipPlayable.Create(playableGraph, forward);
        backPlayableClip = AnimationClipPlayable.Create(playableGraph, backward);
        leftPlayableClip = AnimationClipPlayable.Create(playableGraph, left);
        rightPlayableClip = AnimationClipPlayable.Create(playableGraph, right);
        idlePlayableClip = AnimationClipPlayable.Create(playableGraph, idle);
        dashPlayableClip = AnimationClipPlayable.Create(playableGraph, dash);


        playableGraph.Connect(runPlayableClip, 0, movementBlendPlayable, 0);

        playableGraph.Connect(backPlayableClip, 0, movementBlendPlayable, 1);
        playableGraph.Connect(leftPlayableClip, 0, movementBlendPlayable, 2);
        playableGraph.Connect(rightPlayableClip, 0, movementBlendPlayable, 3);
        playableGraph.Connect(idlePlayableClip, 0, movementBlendPlayable, 4);

        playableGraph.Connect(movementBlendPlayable, 0, mixerPlayable, 0);
        mixerPlayable.SetInputWeight(0, 1.0f);
        mixerPlayable.SetInputWeight(1, 1.0f);
        mixerPlayable.SetInputWeight(2, 1.0f);
       


        // Plays the Graph.

        playableGraph.Play();
    }

    void Update()

    {
        input.Gameplay.Movement.ReadValue<Vector2>();
        move = input.Gameplay.Movement.ReadValue<Vector2>();
        foo = gameObject.transform.forward;
        bla = new Vector3(move.x, 0, move.y);
        pah = Vector3.Cross(bla, foo);
        weightY = pah.z;
        weightX = pah.x;
        movementBlendPlayable.SetInputWeight(0, pah.y);
        movementBlendPlayable.SetInputWeight(1, -pah.y);
        movementBlendPlayable.SetInputWeight(2,  -pah.x);
        movementBlendPlayable.SetInputWeight(3, pah.x);
        movementBlendPlayable.SetInputWeight(4, 0);
    }

    private void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()

    {
        // Destroys all Playables and Outputs created by the graph.
        input.Disable();
        playableGraph.Destroy();
    }

    public void AttackAnimation(AnimationClip clip)
    {
        attackPlayableClip = AnimationClipPlayable.Create(playableGraph, clip);
        playableGraph.Connect(attackPlayableClip, 0, mixerPlayable, 1);
        playableGraph.Disconnect(mixerPlayable, 0);
    }

    public void AttackDisconnecter()
    {
        playableGraph.Disconnect(mixerPlayable, 1);
    }

    public void Dasher()
    {
        AnimationDisconnecter();
        playableGraph.Connect(dashPlayableClip, 0, mixerPlayable, 2);
    }
    public void MoveStarter()
    {
        AnimationDisconnecter();
        playableGraph.Connect(movementBlendPlayable, 0, mixerPlayable, 0);
    }

    public void AnimationDisconnecter()
    {
        for (int i = 0; i < mixerPlayable.GetInputCount(); i++)
        {
            playableGraph.Disconnect(mixerPlayable, i);
        }
    }
    
}