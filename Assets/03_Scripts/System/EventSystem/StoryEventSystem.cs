using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StoryEventSystem : MonoBehaviour
{
    public event System.Action progress;
    public event System.Action nextStory;
    public static StoryEventSystem instance;

    private void Awake()
    {
        instance = this;
    }

    public void Progress()
    {
        if (progress != null)
            progress();
    }

    public void NextStory(){
        if(nextStory != null)
            nextStory();
    }
}
