using UnityEngine;
using UnityEngine.SceneManagement;

public class BedInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private KeyCode key = KeyCode.F;
    [SerializeField] private string requiredQuestId = "tutorial";
    [SerializeField] private string nextSceneName = "NextScene";
    [SerializeField] private SleepCutscene sleepCutscene;

    public KeyCode InteractionKey => key;

    public string GetPromptText()
    {
        if (QuestManager.Instance == null) return "";
        if (!QuestManager.Instance.IsQuestCompleted(requiredQuestId)) return ""; // hidden until done
        return $"Press {key} to Sleep";
    }

    public void Interact(PlayerInteractor interactor)
    {
        if (QuestManager.Instance == null) return;
        if (!QuestManager.Instance.IsQuestCompleted(requiredQuestId)) return;

        if (sleepCutscene != null) sleepCutscene.Play();
    }
}