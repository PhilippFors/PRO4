using System;
using System.Collections.Generic;
using FMOD;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;
using UnityEngine.Video;
using Debug = UnityEngine.Debug;


public class AnimatorController : MonoBehaviour
{
    [HideInInspector] public PlayerControls input;
    [HideInInspector] public Vector2 move;
    [HideInInspector] public Vector2 gamepadRotate;
    private PlayerStateMachine controller => gameObject.GetComponent<PlayerStateMachine>();
    private AttackStateMachine attack => gameObject.GetComponent<AttackStateMachine>();
    private Animator child => gameObject.GetComponent<Animator>();
    private AnimatorStateMachine child2 => gameObject.GetComponent<AnimatorStateMachine>();

    public AnimationClip forward, backward, right, left, idle, dash;

    public float transWeight;

    PlayableGraph playableGraph;

    AnimationMixerPlayable movementBlendPlayable;
    AnimationMixerPlayable mixerPlayable;
    AnimationMixerPlayable attackBlendPlayable;

    private Vector3 foo;
    private Vector3 bla;
    private Vector3 pah;

    private float timeStartedLerping;
    private bool shouldBlend;
    
    protected AnimatorOverrideController animatorOverrideController;
    private void Awake()
    {
        input = new PlayerControls();
    }

    void Start()

    {
       
        
        // Plays the Graph.
    }

    void Update()
    {
        // move = input.Gameplay.Movement.ReadValue<Vector2>();
        // foo = gameObject.transform.forward;
        // bla = new Vector3(move.x, 0, move.y);
        // pah = Vector3.Cross(bla, foo);
        // weightY = pah.z;
        // weightX = pah.x;
        float posx = 0;
        float posy = 0;
        if (controller.isMoving)
        {
            float deg = Vector3.Angle(transform.right, controller.currentMoveDirection.normalized);
            if (Vector3.Dot(transform.forward, controller.currentMoveDirection.normalized) < 0)
            {
                deg = 360 - deg;
            }
            posx = Mathf.Cos(deg * Mathf.Deg2Rad);
            posy = Mathf.Sin(deg * Mathf.Deg2Rad);
            // Debug.Log(deg);
        }

        // Debug.Log(posx.ToString() + ", " + posy.ToString());

        // movementBlendPlayable.SetInputWeight(0, posy);

        // movementBlendPlayable.SetInputWeight(1, -posy);

        // movementBlendPlayable.SetInputWeight(2, -posx);

        // movementBlendPlayable.SetInputWeight(3, posx);

        // movementBlendPlayable.SetInputWeight(4, 0);

        child.SetFloat("X", posx);
        child.SetFloat("Y", posy);

        if (shouldBlend)
        {
            //transWeight = Blender(timeStartedLerping);
        }
        
    }

    private void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()

    {
        // Destroys all Playables and Outputs created by the graph.nimm lieber
        input.Disable();
    }
    
    public void AttackAnimation(AnimationClip attackClip, AnimationClip pauseClip, AnimationClip pullbackClip)
    {
        animatorOverrideController = new AnimatorOverrideController(child.runtimeAnimatorController);
        child.runtimeAnimatorController = animatorOverrideController;
        animatorOverrideController["Dagger_H1_att"] = attackClip;
        animatorOverrideController["Dagger_H1_pause"] = pauseClip;
        animatorOverrideController["Dagger_H1_pullback"] = pullbackClip;
        child.SetTrigger("attack");
    }

    public void AnimationDisconnecter()
    {
        for (int i = 0; i < mixerPlayable.GetInputCount(); i++)
        {
            playableGraph.Disconnect(mixerPlayable, i);
        }
    }

    public float Blender(float timeStartedLerping, float lerpTime = 0.1f)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;
        var result = Mathf.Lerp(0, 1, percentageComplete);
        if (result >= 1)
        {
            shouldBlend = false;
        }
        return result;
    }

    public void BlendStarter()
    {
       
    }

}