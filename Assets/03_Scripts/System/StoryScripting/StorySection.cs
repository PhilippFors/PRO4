using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author(mainAuthor = "Philipp Forstner")]
public abstract class StorySection : ScriptableObject
{
    public bool started;
    public bool finished;
    public int AreaID;
    public bool multiArea = false;
    public StoryScripting st;

    public abstract void ProgressStory();

    public abstract void StorySecUpdate();

    public virtual void StoryEnter(StoryScripting script)
    {
        st = script;
        StoryEventSystem.instance.progress += ProgressStory;
        st.levelManager.currentObjective.letAreaFinish = false;
    }
    public virtual void StoryExit()
    {
        st = null;
        StoryEventSystem.instance.progress -= ProgressStory;
    }

    private void OnDisable()
    {
        started = false;
        finished = false;
    }
}
