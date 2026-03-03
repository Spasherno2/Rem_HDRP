using UnityEngine;

public class RadioInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactableId = "tutorial_radio";
    [SerializeField] private KeyCode key = KeyCode.F;
    [SerializeField] private AudioSource radioSource;

    private bool isOn = true;

    public KeyCode InteractionKey => key;

    private void Awake()
    {
        if (radioSource == null) radioSource = GetComponent<AudioSource>();
        isOn = radioSource != null && radioSource.isPlaying;
    }

    public string GetPromptText()
    {
        if (!isOn) return ""; // hide once off
        return $"Press {key} to Turn off Radio";
    }

    public void Interact(PlayerInteractor interactor)
    {
        if (!isOn) return;

        if (radioSource != null) radioSource.Stop();
        isOn = false;

        GameEvents.RaiseInteractableUsed(interactableId);
    }
}