using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    [TextArea] public string description;

    [Header("In-hand visuals")]
    public GameObject inHandPrefab;

    [Header("SFX (per item)")]
    public AudioClip pickupSfx;
    public AudioClip equipSfx;
    public AudioClip unequipSfx;

    [Range(0f, 1f)] public float pickupVolume = 1f;
    [Range(0f, 1f)] public float equipVolume = 1f;
    [Range(0f, 1f)] public float unequipVolume = 1f;
}