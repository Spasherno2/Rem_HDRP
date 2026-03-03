using TMPro;
using UnityEngine;

public class InteractPromptUI : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private TMP_Text promptText;

    private void Awake()
    {
        if (root == null) root = gameObject;
        Hide();
    }

    public void Show(string text)
    {
        if (promptText != null) promptText.text = text;
        root.SetActive(true);
    }

    public void Hide()
    {
        root.SetActive(false);
    }
}