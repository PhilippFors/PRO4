using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author(mainAuthor = "Philipp Forstner")]
[CreateAssetMenu(fileName = "Outofboundstrigger", menuName ="StoryScript/Triggers/OutofBoundsTrigger")]
public class OutOfBoundStrigger : TriggerBehaviour
{
    public override void Execute(Collider other, GenericTrigger t = null)
    {
        PlayerStatistics obj = other.GetComponent<PlayerStatistics>();
        if (obj != null)
            LevelEventSystem.instance.ReturnToCheckpoint(obj);
    }
}