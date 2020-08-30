using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public TutorialInteractable currentInteractable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            if (currentInteractable != null)
                currentInteractable.Interact();
    }
}
