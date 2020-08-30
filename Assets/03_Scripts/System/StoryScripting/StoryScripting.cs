using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScripting : MonoBehaviour
{
    public LevelManager levelManager;
    public SpawnManager spawnManager;
    public AIManager aiManager;
    public PlayerStateMachine playerMovement;

    public int currentStoryID;
    public StorySection currentStory;
    public StorySection[] storyList;

    private void Start()
    {
        StoryEventSystem.instance.nextStory += StartStorySection;
    }
    private void Update()
    {
        if (currentStory != null)
            currentStory.CheckStoryUpdate();
    }
    public void SwitchStorySection()
    {
        storyList[currentStoryID].StoryExit();
        currentStory.finished = true;
        currentStory = null;
    }

    public void StartStorySection()
    {
        currentStory = GetNextStoryObjective();
        currentStory.started = true;
        currentStory.StoryEnter(this);
    }

    public void FinishStory()
    {
        if (HasNextStorySection())
            currentStoryID++;

        // StartCoroutine(SaveGame());
    }

    StorySection GetNextStoryObjective()
    {

        return storyList[currentStoryID];

    }

    bool HasNextStorySection()
    {
        return currentStoryID + 1 < storyList.Length;
    }

}
