using System;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectiveType
{
    UseItem,
    InteractWithId
}

[Serializable]
public class ObjectiveDefinition
{
    public string description;
    public ObjectiveType type;

    public ItemData targetItem;          // for UseItem
    public string targetInteractableId;  // for InteractWithId
}

[CreateAssetMenu(menuName = "Quests/Quest Definition")]
public class QuestDefinition : ScriptableObject
{
    public string questId = "tutorial";
    public string questTitle = "Tutorial";
    public List<ObjectiveDefinition> objectives = new List<ObjectiveDefinition>();
}