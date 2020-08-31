using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AInteractable : MonoBehaviour
{
    public abstract void Interact();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInteraction>())
            other.GetComponent<PlayerInteraction>().currentInteractable = this;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerInteraction>())
            other.GetComponent<PlayerInteraction>().currentInteractable = null;
    }
}
