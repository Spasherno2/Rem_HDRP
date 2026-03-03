using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepCutscene : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform lookTarget;

    [Header("Disable player scripts during cutscene")]
    [SerializeField] private MonoBehaviour[] scriptsToDisable; // e.g., movement, PlayerInteractor, mouse look, etc.

    [Header("Timing")]
    [SerializeField] private float lookDuration = 1.2f;
    [SerializeField] private float rotateSpeed = 6f;
    [SerializeField] private float fadeOutTime = 0.8f;

    [Header("Scene")]
    [SerializeField] private string nextSceneName = "NextScene";

    [Header("Zoom")]
    [SerializeField] private bool zoomIn = true;
    [SerializeField] private float startFov = 60f;
    [SerializeField] private float zoomFov = 35f;      // smaller = more zoom
    [SerializeField] private float zoomDuration = 1.2f;
    [SerializeField] private AnimationCurve zoomCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private bool isPlaying;

    public void Play()
    {
        if (isPlaying) return;
        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence()
    {
        isPlaying = true;

        // Disable control scripts
        foreach (var s in scriptsToDisable)
            if (s != null) s.enabled = false;

        // Turn camera to look at target smoothly
        if (playerCamera != null && lookTarget != null)
        {
            float t = 0f;
            while (t < lookDuration)
            {
                t += Time.deltaTime;

                Vector3 dir = (lookTarget.position - playerCamera.transform.position).normalized;
                Quaternion targetRot = Quaternion.LookRotation(dir);

                playerCamera.transform.rotation =
                    Quaternion.Slerp(playerCamera.transform.rotation, targetRot, Time.deltaTime * rotateSpeed);

                yield return null;
            }
        }
        // Set initial FOV
        if (playerCamera != null)
            playerCamera.fieldOfView = startFov;

        // Smooth zoom
        if (playerCamera != null && zoomIn)
        {
            float t = 0f;
            while (t < zoomDuration)
            {
                t += Time.deltaTime;
                float u = Mathf.Clamp01(t / zoomDuration);
                float k = zoomCurve.Evaluate(u);
                playerCamera.fieldOfView = Mathf.Lerp(startFov, zoomFov, k);
                yield return null;
            }
        }
        // Fade out to black
        if (ScreenFader.Instance != null)
            yield return ScreenFader.Instance.FadeOut(fadeOutTime);

        // Load next scene (ScreenFader persists and will fade in on Start)
        SceneManager.LoadScene(nextSceneName);
    }
}