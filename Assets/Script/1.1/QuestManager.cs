using System;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    [SerializeField] private QuestDefinition startingQuest;

    public event Action OnQuestUpdated;

    public QuestDefinition CurrentQuest { get; private set; }
    public int CurrentObjectiveIndex { get; private set; }
    public bool IsQuestComplete { get; private set; }

    private bool[] completed;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        GameEvents.ItemUsed += HandleItemUsed;
        GameEvents.InteractableUsed += HandleInteractableUsed;
    }

    private void OnDisable()
    {
        GameEvents.ItemUsed -= HandleItemUsed;
        GameEvents.InteractableUsed -= HandleInteractableUsed;
    }

    private void Start()
    {
        if (startingQuest != null) StartQuest(startingQuest);
    }

    public void StartQuest(QuestDefinition quest)
    {
        CurrentQuest = quest;
        CurrentObjectiveIndex = 0;
        IsQuestComplete = false;

        completed = new bool[quest.objectives.Count];
        OnQuestUpdated?.Invoke();
    }

    public bool IsObjectiveCompleted(int index)
    {
        if (completed == null || index < 0 || index >= completed.Length) return false;
        return completed[index];
    }

    public bool IsQuestCompleted(string questId)
    {
        return CurrentQuest != null && CurrentQuest.questId == questId && IsQuestComplete;
    }

    private void CompleteCurrentObjective()
    {
        if (CurrentQuest == null || IsQuestComplete) return;

        completed[CurrentObjectiveIndex] = true;
        CurrentObjectiveIndex++;

        if (CurrentObjectiveIndex >= CurrentQuest.objectives.Count)
            IsQuestComplete = true;

        OnQuestUpdated?.Invoke();
    }

    private ObjectiveDefinition GetCurrentObjective()
    {
        if (CurrentQuest == null) return null;
        if (IsQuestComplete) return null;
        if (CurrentObjectiveIndex < 0 || CurrentObjectiveIndex >= CurrentQuest.objectives.Count) return null;
        return CurrentQuest.objectives[CurrentObjectiveIndex];
    }

    private void HandleItemUsed(ItemData item)
    {
        var obj = GetCurrentObjective();
        if (obj == null) return;

        if (obj.type == ObjectiveType.UseItem && obj.targetItem == item)
            CompleteCurrentObjective();
    }

    private void HandleInteractableUsed(string id)
    {
        var obj = GetCurrentObjective();
        if (obj == null) return;

        if (obj.type == ObjectiveType.InteractWithId && obj.targetInteractableId == id)
            CompleteCurrentObjective();
    }
}