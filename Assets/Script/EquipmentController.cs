using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Transform handAnchor;
    [SerializeField] private PlayerSFX sfx;

    private int equippedSlot = -1;
    private GameObject currentInHand;
    private ItemData equippedItem;

    private void Awake()
    {
        if (inventory == null) inventory = GetComponent<Inventory>();
        if (sfx == null) sfx = GetComponent<PlayerSFX>();
    }

    public int EquippedSlot => equippedSlot;

    public void EquipSlot(int slotIndex)
    {
        if (inventory == null) return;

        // Toggle off
        if (equippedSlot == slotIndex)
        {
            Unequip();
            return;
        }

        ItemData item = inventory.GetSlot(slotIndex);

        // If empty slot, just unequip
        if (item == null)
        {
            Unequip();
            return;
        }

        // Switch items
        Unequip();

        equippedSlot = slotIndex;
        equippedItem = item;

        if (sfx != null) sfx.Play(item.equipSfx, item.equipVolume);

        if (item.inHandPrefab != null && handAnchor != null)
        {
            currentInHand = Instantiate(item.inHandPrefab, handAnchor);
            currentInHand.transform.localPosition = Vector3.zero;
            currentInHand.transform.localRotation = Quaternion.identity;
            currentInHand.transform.localScale = Vector3.one;
        }
    }

    public void Unequip()
    {
        if (equippedItem != null && sfx != null)
            sfx.Play(equippedItem.unequipSfx, equippedItem.unequipVolume);

        equippedSlot = -1;
        equippedItem = null;

        if (currentInHand != null) Destroy(currentInHand);
        currentInHand = null;
    }
}