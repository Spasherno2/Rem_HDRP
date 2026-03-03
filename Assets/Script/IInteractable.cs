using UnityEngine;

public interface IInteractable
{
    KeyCode InteractionKey { get; }
    string GetPromptText();
    void Interact(PlayerInteractor interactor);
}