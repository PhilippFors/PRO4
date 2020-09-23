using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Outofboundstrigger", menuName ="StoryScript/Triggers/OutofBoundsTrigger")]
public class OutOfBoundStrigger : TriggerBehaviour
{
    public override void Execute(Collider other, GenericTrigger t = null)
    {
        PlayerBody obj = other.GetComponent<PlayerBody>();
        if (obj != null)
            LevelEventSystem.instance.ReturnToCheckpoint(obj);
    }
}