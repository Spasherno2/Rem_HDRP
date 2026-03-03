using UnityEngine;

public class FlashlightToggle : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private KeyCode toggleKey = KeyCode.F; // change to T if you use F for interact

    [Header("Light")]
    [SerializeField] private Light flashlightLight;
    [SerializeField] private bool startOn = true;

    [Header("SFX")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip toggleOnSfx;
    [SerializeField] private AudioClip toggleOffSfx;
    [Range(0f, 1f)][SerializeField] private float volume = 1f;

    private void Awake()
    {
        if (flashlightLight == null) flashlightLight = GetComponentInChildren<Light>(true);
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SetLight(startOn);
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            SetLight(!flashlightLight.enabled);
        }
    }

    private void SetLight(bool on)
    {
        if (flashlightLight != null) flashlightLight.enabled = on;

        if (audioSource != null)
        {
            var clip = on ? toggleOnSfx : toggleOffSfx;
            if (clip != null) audioSource.PlayOneShot(clip, volume);
        }
    }
}