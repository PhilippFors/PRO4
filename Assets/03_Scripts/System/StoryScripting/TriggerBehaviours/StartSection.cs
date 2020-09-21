using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StartSectoin", menuName = "StoryScript/Triggers/StartSection")]
public class StartSection : TriggerBehaviour
{
    public override void Execute(Collider other, GenericTrigger t)
    {
        StoryEventSystem.instance.NextStory();
        // t.StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        StoryEventSystem.instance.ShowPrompt();
    }
}
