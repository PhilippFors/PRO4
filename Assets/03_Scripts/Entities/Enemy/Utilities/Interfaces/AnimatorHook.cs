using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Author("Philipp Forstner")]
public abstract class AnimatorHook : MonoBehaviour
{
    Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void StartAttackAnim(string attack)
    {

    }

    public virtual void StartAttackAnim(AnimationClip clip)
    {
        //Put animationclip into state
        //Play clip
    }

    public virtual void CancleAnim()
    {

    }

}

