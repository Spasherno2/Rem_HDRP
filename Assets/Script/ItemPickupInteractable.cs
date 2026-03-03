using UnityEngine;

public class ItemPickupInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData item;
    [SerializeField] private KeyCode key = KeyCode.E;

    public KeyCode InteractionKey => key;

    public string GetPromptText()
    {
        string name = item != null ? item.itemName : "Item";
        return $"Press {key} to Pick up {name}";
    }

    public void Interact(PlayerInteractor interactor)
    {
        if (interactor == null || interactor.Inventory == null || item == null) return;

        if (interactor.Inventory.TryAdd(item, out int slot))
        {
            var sfx = interactor.GetComponent<PlayerSFX>();
            if (sfx != null) sfx.Play(item.pickupSfx, item.pickupVolume);

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Inventory is full!");
        }
        GameEvents.RaiseItemPickedUp(item);

        if (item.autoEquipOnPickup && interactor.Equipment != null)
        {
            interactor.Equipment.EquipSlot(slot);
        }
    }
}