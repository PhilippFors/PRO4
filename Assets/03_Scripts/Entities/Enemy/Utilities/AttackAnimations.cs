using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [Author("Philipp Forstner")]
    [System.Serializable]
    public class AttackAnimations
    {
        public AnimationClip clip;
        public string animationName => clip.name;
        public float damageFrameStart;
        public float damageFrameEnd;
        public float attRange;
        public float attackWidth;
        public float clipLength => clip != null ? clip.length : 0;
    }
}
