using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    private void Awake()
    {
        if (source == null) source = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip, float volume = 1f)
    {
        if (source == null || clip == null) return;
        source.PlayOneShot(clip, Mathf.Clamp01(volume));
    }
}