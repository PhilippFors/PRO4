using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StorySection : ScriptableObject
{
    public bool started;
    public bool finished;
    public int AreaID;
    public bool multiArea = false;
    public StoryScripting storyScript;

    public abstract void ProgressStory();

    public abstract void CheckStoryUpdate();

    public virtual void StoryEnter(StoryScripting script)
    {
        storyScript = script;
        StoryEventSystem.instance.progress += ProgressStory;
        storyScript.levelManager.currentObjective.letAreaFinish = false;
    }
    public virtual void StoryExit()
    {
        storyScript = null;
        StoryEventSystem.instance.progress -= ProgressStory;
    }
}
