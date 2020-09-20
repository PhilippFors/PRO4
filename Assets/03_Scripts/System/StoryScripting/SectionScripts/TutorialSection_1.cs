using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New DestructTutSection", menuName = "StoryScript/TutorialSection_1")]
public class TutorialSection_1 : StorySection
{
    public string tutDescription;

    public override void StorySecUpdate()
    {

    }

    public override void ProgressStory()
    {
        st.uiManager.DisablePrompt();
        st.playerMovement.input.Gameplay.Enable();
        st.SwitchStorySection();
    }

    public void ShowTutScreen()
    {
        st.uiManager.ShowPrompt(tutDescription);
        st.playerMovement.input.Gameplay.Disable();
    }

    public override void StoryEnter(StoryScripting script)
    {
        base.StoryEnter(script);
        StoryEventSystem.instance.showPrompt += ShowTutScreen;
        
    }

    public override void StoryExit()
    {
        base.StoryExit();
        StoryEventSystem.instance.showPrompt -= ShowTutScreen;
        
    }
}
