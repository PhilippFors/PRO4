using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShowPromptTrigger", menuName = "StoryScript/Triggers/ShowPromptTrigger")]
public class ShowPrompt : TriggerBehaviour
{
    public override void Execute(Collider other, StoryTrigger t = null)
    {
        StoryEventSystem.instance.ShowPrompt();
    }
}
