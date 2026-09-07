using System;

[Serializable]
public class QuestDefinition
{
    public string id;
    public string title;
    public string description;
    public QuestType type;
    public string targetId;
    public int requiredAmount;
    public Reward reward;
    public bool isAchievement;
    public string[] requiredQuestIds;
}

[Serializable]
public class Reward
{
    public int experience;
    public int gold;
    public ItemRewardData[] items;
}

[Serializable]
public class ItemRewardData
{
    public string itemId;
    public int amount;
}