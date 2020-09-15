using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerBehaviour : ScriptableObject
{
    public abstract void Execute(Collider other, StoryTrigger t = null);
}
