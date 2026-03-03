using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    [TextArea] public string description;

    [Header("In-hand visuals")]
    public GameObject inHandPrefab;

    [Header("Pickup/Equip SFX")]
    public AudioClip pickupSfx;
    public AudioClip equipSfx;
    public AudioClip unequipSfx;
    [Range(0f, 1f)] public float pickupVolume = 1f;
    [Range(0f, 1f)] public float equipVolume = 1f;
    [Range(0f, 1f)] public float unequipVolume = 1f;

    [Header("Use (per item)")]
    public bool isConsumable = false;          // Pizza = true
    public bool autoEquipOnPickup = false;     // Pizza tutorial = true
    public AudioClip useSfx;
    [Range(0f, 1f)] public float useVolume = 1f;
}