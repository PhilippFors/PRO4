using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyTutorialSection", menuName = "StoryScript/TutorialSection_0")]
public class TutorialSection_0 : StorySection
{
    public string tutDescription;
    public override void StorySecUpdate()
    {

    }
    
    public void ShowTutScreen()
    {
        st.uiManager.ShowPrompt(tutDescription);
        st.playerMovement.input.Gameplay.Disable();
    }

    public override void ProgressStory()
    {
        st.uiManager.DisablePrompt();
        st.playerMovement.input.Gameplay.Enable();
        foreach (EnemyBody e in st.spawnManager.enemyCollection.entityList)
            EventSystem.instance.ActivateAI(e);

        st.spawnManager.scriptedSpawn = false;
        st.SwitchStorySection();
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
