using UnityEngine;

public class PlayerItemUser : MonoBehaviour
{
    [SerializeField] private KeyCode useKey = KeyCode.Mouse0; // left click
    [SerializeField] private Inventory inventory;
    [SerializeField] private EquipmentController equipment;
    [SerializeField] private PlayerSFX sfx;

    private void Awake()
    {
        if (inventory == null) inventory = GetComponent<Inventory>();
        if (equipment == null) equipment = GetComponent<EquipmentController>();
        if (sfx == null) sfx = GetComponent<PlayerSFX>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(useKey))
            TryUseEquipped();
    }

    private void TryUseEquipped()
    {
        if (inventory == null || equipment == null) return;

        int slot = equipment.EquippedSlot;
        if (slot < 0) return;

        ItemData item = inventory.GetSlot(slot);
        if (item == null) return;

        // Only consumables are "used" in this simple system (pizza etc.)
        if (!item.isConsumable) return;

        if (sfx != null) sfx.Play(item.useSfx, item.useVolume);

        // consume
        inventory.ClearSlot(slot);
        equipment.Unequip();

        GameEvents.RaiseItemUsed(item);
    }
}