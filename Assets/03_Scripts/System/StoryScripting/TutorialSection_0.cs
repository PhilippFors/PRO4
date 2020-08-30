using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TutorialSection", menuName = "StoryScript/TutorialSection_0")]
public class TutorialSection_0 : StorySection
{

    public override void CheckStoryUpdate()
    {
        if (storyScript.playerMovement.input.Gameplay.LeftAttack.triggered)
            ProgressStory();
    }

    public override void ProgressStory()
    {
        foreach (EnemyBody e in storyScript.spawnManager.enemyCollection.entityList)
            EventSystem.instance.ActivateAI(e);

        storyScript.spawnManager.scriptedSpawn = false;

    }

    public override void StoryEnter(StoryScripting script)
    {
        storyScript = script;
        StoryEventSystem.instance.progress += ProgressStory;
        storyScript.spawnManager.scriptedSpawn = true;
    }

}
