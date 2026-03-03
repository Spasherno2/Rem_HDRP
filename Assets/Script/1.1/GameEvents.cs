using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<ItemData> ItemPickedUp;
    public static event Action<ItemData> ItemUsed;
    public static event Action<string> InteractableUsed;

    public static void RaiseItemPickedUp(ItemData item) => ItemPickedUp?.Invoke(item);
    public static void RaiseItemUsed(ItemData item) => ItemUsed?.Invoke(item);
    public static void RaiseInteractableUsed(string id) => InteractableUsed?.Invoke(id);
}