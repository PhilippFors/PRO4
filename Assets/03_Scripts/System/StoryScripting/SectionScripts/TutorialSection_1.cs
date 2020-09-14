using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TutorialSection_1 : StorySection
{
    public string tutDescription;
    public override void ProgressStory()
    {   
        storyScript.uiManager.DisablePrompt();
        foreach (EnemyBody e in storyScript.spawnManager.enemyCollection.entityList)
            EventSystem.instance.ActivateAI(e);

        storyScript.spawnManager.scriptedSpawn = false;
        storyScript.SwitchStorySection();
    }

    public override void StorySecUpdate()
    {

    }

    public void ShowTutScreen()
    {
        storyScript.uiManager.ShowPrompt(tutDescription);
    }

    public override void StoryEnter(StoryScripting script)
    {
        base.StoryEnter(script);
        StoryEventSystem.instance.showPrompt += ShowTutScreen;
        storyScript.spawnManager.scriptedSpawn = true;
    }

    public override void StoryExit()
    {
        base.StoryExit();
        StoryEventSystem.instance.showPrompt -= ShowTutScreen;
    }
}
