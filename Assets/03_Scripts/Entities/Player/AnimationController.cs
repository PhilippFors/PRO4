using System;
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
    private Animator child => gameObject.GetComponentInChildren<Animator>();

    public AnimationClip forward;
    public AnimationClip backward;
    public AnimationClip right;
    public AnimationClip left;
    public AnimationClip idle;

    public AnimationClipPlayable attackPlayableClip;


    public float weightX;
    public float weightY;

    PlayableGraph playableGraph;

    AnimationMixerPlayable movementBlendPlayable;
    AnimationMixerPlayable mixerPlayable;

    private void Awake()
    {
        input = new PlayerControls();
    }

    void Start()

    {
        // Creates the graph, the mixer and binds them to the Animator.

        playableGraph = PlayableGraph.Create();

        var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", child);
        Debug.Log(GetComponentInChildren<Animator>());

        movementBlendPlayable = AnimationMixerPlayable.Create(playableGraph, 5);
        mixerPlayable = AnimationMixerPlayable.Create(playableGraph, 2);

        playableOutput.SetSourcePlayable(mixerPlayable);

        // Creates AnimationClipPlayable and connects them to the mixer.

        var runPlayableClip = AnimationClipPlayable.Create(playableGraph, forward);

        var backPlayableClip = AnimationClipPlayable.Create(playableGraph, backward);

        var leftPlayableClip = AnimationClipPlayable.Create(playableGraph, left);

        var rightPlayableClip = AnimationClipPlayable.Create(playableGraph, right);
        var idlePlayableClip = AnimationClipPlayable.Create(playableGraph, idle);


        playableGraph.Connect(runPlayableClip, 0, movementBlendPlayable, 0);

        playableGraph.Connect(backPlayableClip, 0, movementBlendPlayable, 1);
        playableGraph.Connect(leftPlayableClip, 0, movementBlendPlayable, 2);
        playableGraph.Connect(rightPlayableClip, 0, movementBlendPlayable, 3);
        playableGraph.Connect(idlePlayableClip, 0, movementBlendPlayable, 4);

        playableGraph.Connect(movementBlendPlayable, 0, mixerPlayable, 0);
        mixerPlayable.SetInputWeight(0, 1.0f);
        mixerPlayable.SetInputWeight(1, 1.0f);


        // Plays the Graph.

        playableGraph.Play();
    }

    void Update()

    {
        input.Gameplay.Movement.ReadValue<Vector2>();
        move = input.Gameplay.Movement.ReadValue<Vector2>();
        Vector3 foo = gameObject.transform.forward;
        Vector3 bla = new Vector2(foo.x, foo.z);
        Vector2 pah = bla * move;

        // weightY = Mathf.Clamp01(y);
        // weightX = Mathf.Clamp01(x);
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
        movementBlendPlayable.Pause();

    }

    public void AttackDisconnecter()
    {
        playableGraph.Disconnect(mixerPlayable, 1);
    }

    public void MoveStarter()
    {
        movementBlendPlayable.Play();
    }
}