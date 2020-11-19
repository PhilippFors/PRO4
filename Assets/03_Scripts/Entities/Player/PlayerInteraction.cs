using UnityEngine;
[Author(mainAuthor = "Philipp Forstner")]
public class PlayerInteraction : MonoBehaviour
{
    public AInteractable currentInteractable;
    [SerializeField] PlayerStateMachine player;

    private void Start()
    {
        player.input.Gameplay.Interact.performed += ctx => InteractWith();
    }

    public void InteractWith()
    {
        if (currentInteractable != null)
            currentInteractable.Interact();
    }
}
