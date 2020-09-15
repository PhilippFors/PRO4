using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TutorialSection_1 : StorySection
{
    public string tutDescription;
    public override void ProgressStory()
    {   
        st.uiManager.DisablePrompt();
        foreach (EnemyBody e in st.spawnManager.enemyCollection.entityList)
            EventSystem.instance.ActivateAI(e);

        st.spawnManager.scriptedSpawn = false;
        st.SwitchStorySection();
    }

    public override void StorySecUpdate()
    {

    }

    public void ShowTutScreen()
    {
        st.uiManager.ShowPrompt(tutDescription);
    }

    public override void StoryEnter(StoryScripting script)
    {
        base.StoryEnter(script);
        StoryEventSystem.instance.showPrompt += ShowTutScreen;
        st.spawnManager.scriptedSpawn = true;
    }

    public override void StoryExit()
    {
        base.StoryExit();
        StoryEventSystem.instance.showPrompt -= ShowTutScreen;
    }
}
