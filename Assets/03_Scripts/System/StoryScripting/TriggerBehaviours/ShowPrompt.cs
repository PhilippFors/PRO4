using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Author(mainAuthor = "Philipp Forstner")]

[CreateAssetMenu(fileName = "ShowPromptTrigger", menuName = "StoryScript/Triggers/ShowPromptTrigger")]
public class ShowPrompt : TriggerBehaviour
{
    public override void Execute(Collider other, GenericTrigger t = null)
    {
        StoryEventSystem.instance.ShowPrompt();
    }
}
