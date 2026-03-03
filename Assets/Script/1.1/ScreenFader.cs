using System.Collections;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance { get; private set; }

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float defaultFadeTime = 0.8f;
    [SerializeField] private bool fadeInOnSceneStart = true;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();

       
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = false;
    }

    private void Start()
    {
        if (fadeInOnSceneStart)
            StartCoroutine(FadeIn(defaultFadeTime));
    }

    public IEnumerator FadeIn(float time = -1f)
    {
        if (time < 0f) time = defaultFadeTime;
        yield return FadeTo(0f, time);
    }

    public IEnumerator FadeOut(float time = -1f)
    {
        if (time < 0f) time = defaultFadeTime;
        yield return FadeTo(1f, time);
    }

    private IEnumerator FadeTo(float target, float time)
    {
        canvasGroup.blocksRaycasts = true;

        float start = canvasGroup.alpha;
        float t = 0f;

        while (t < time)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, target, t / time);
            yield return null;
        }

        canvasGroup.alpha = target;

   
        canvasGroup.blocksRaycasts = canvasGroup.alpha > 0.001f;
    }
}