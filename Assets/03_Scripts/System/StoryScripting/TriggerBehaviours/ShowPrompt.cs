using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPrompt : TriggerBehaviour
{
    public override void Execute(Collider other, StoryTrigger t = null)
    {
        StoryEventSystem.instance.ShowPrompt();
    }
}
