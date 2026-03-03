using UnityEngine;

public class Inventory : MonoBehaviour
{
    public const int SlotCount = 10;

    [SerializeField] private ItemData[] slots = new ItemData[SlotCount];

    public ItemData GetSlot(int index)
    {
        if (index < 0 || index >= SlotCount) return null;
        return slots[index];
    }

    public bool TryAdd(ItemData item, out int placedIndex)
    {
        placedIndex = -1;
        if (item == null) return false;

        for (int i = 0; i < SlotCount; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = item;
                placedIndex = i;
                return true;
            }
        }
        return false; // full
    }

    public void ClearSlot(int index)
    {
        if (index < 0 || index >= SlotCount) return;
        slots[index] = null;
    }
}