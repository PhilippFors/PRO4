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
    [SerializeField] StorySection[] storyList;

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
        CheckForNextSection();
    }

    void CheckForNextSection()
    {
        if (currentStoryID + 1 < storyList.Length)
            if (storyList[currentStoryID + 1].AreaID == storyList[currentStoryID].AreaID)
            {
                FinishStorySection(false);
                StartStorySection();
            }
            else
            {
                FinishStorySection(true);
            }
        else
            FinishStorySection(true);

    }

    public void StartStorySection()
    {
        currentStory = GetNextStoryObjective();
        currentStory.started = true;
        currentStory.StoryEnter(this);
    }

    public void FinishStorySection(bool endObjective)
    {
        if (HasNextStorySection())
            currentStoryID++;

        if (endObjective)
            levelManager.currentObjective.letAreaFinish = true;
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
