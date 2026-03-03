using System.Text;
using TMPro;
using UnityEngine;

public class QuestUIPage : MonoBehaviour
{
    [SerializeField] private KeyCode toggleKey = KeyCode.Tab;
    [SerializeField] private GameObject pageRoot;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text objectivesText;

    private void Start()
    {
        if (pageRoot != null) pageRoot.SetActive(false);
        Refresh();
    }

    private void OnEnable()
    {
        if (QuestManager.Instance != null)
            QuestManager.Instance.OnQuestUpdated += Refresh;
    }

    private void OnDisable()
    {
        if (QuestManager.Instance != null)
            QuestManager.Instance.OnQuestUpdated -= Refresh;
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey) && pageRoot != null)
        {
            pageRoot.SetActive(!pageRoot.activeSelf);
            Refresh();
        }
    }

    private void Refresh()
    {
        var qm = QuestManager.Instance;
        if (qm == null || qm.CurrentQuest == null)
        {
            if (titleText) titleText.text = "";
            if (objectivesText) objectivesText.text = "";
            return;
        }

        if (titleText) titleText.text = qm.CurrentQuest.questTitle;

        var sb = new StringBuilder();
        for (int i = 0; i < qm.CurrentQuest.objectives.Count; i++)
        {
            bool done = qm.IsObjectiveCompleted(i);
            string box = done ? "[x]" : "[ ]";
            string arrow = (!done && i == qm.CurrentObjectiveIndex && !qm.IsQuestComplete) ? ">> " : "   ";
            sb.AppendLine($"{arrow}{box} Objective {i + 1}: {qm.CurrentQuest.objectives[i].description}");
        }

        if (qm.IsQuestComplete) sb.AppendLine("\nAll objectives complete. Go to bed.");

        if (objectivesText) objectivesText.text = sb.ToString();
    }
}