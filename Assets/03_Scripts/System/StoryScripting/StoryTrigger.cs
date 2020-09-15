using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    public TriggerBehaviour behaviour;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerBody>())
        {
            behaviour.Execute(other, this);
            enabled = false;
        }
    }
}
