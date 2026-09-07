public static class QuestFactory
{
    public static IQuest CreateQuest(QuestData data, string[] requiredQuestIds)
    {
        IReward reward = CreateReward(data.reward);   // использует существующий метод CreateReward(Reward)
        switch (data.questType)
        {
            case QuestType.KillEntity:
                return new KillEntityQuest(data.questId, data.title, data.description, data.targetId, data.requiredAmount,
                    reward, data.isAchievement, requiredQuestIds);
            case QuestType.CollectItem:
                return new CollectItemQuest(data.questId, data.title, data.description, data.targetId, data.requiredAmount,
                    reward, data.isAchievement, requiredQuestIds);
            case QuestType.ReachLocation:
                return new ReachLocationQuest(data.questId, data.title, data.description, data.targetId,
                    reward, data.isAchievement, requiredQuestIds);
            case QuestType.TalkNPC:
                return new TalkNPCQuest(data.questId, data.title, data.description, data.targetId,
                    reward, data.isAchievement, requiredQuestIds);
            default:
                throw new System.Exception("Unknown quest type");
        }
    }
    public static IQuest CreateQuest(QuestDefinition def, string[] requiredQuestIds = null)
    {
        IReward reward = CreateReward(def.reward);
        if (requiredQuestIds == null) requiredQuestIds = def.requiredQuestIds ?? new string[0];

        switch (def.type)
        {
            case QuestType.KillEntity:
                return new KillEntityQuest(def.id, def.title, def.description, def.targetId, def.requiredAmount,
                    reward, def.isAchievement, requiredQuestIds);
            case QuestType.CollectItem:
                return new CollectItemQuest(def.id, def.title, def.description, def.targetId, def.requiredAmount,
                    reward, def.isAchievement, requiredQuestIds);
            case QuestType.ReachLocation:
                return new ReachLocationQuest(def.id, def.title, def.description, def.targetId,
                    reward, def.isAchievement, requiredQuestIds);
            case QuestType.TalkNPC:
                return new TalkNPCQuest(def.id, def.title, def.description, def.targetId,
                    reward, def.isAchievement, requiredQuestIds);
            default:
                throw new System.Exception("Unknown quest type");
        }
    }

    private static IReward CreateReward(Reward rewardDef)
    {
        if (rewardDef == null) return null;
        var composite = new CompositeReward();
        if (rewardDef.experience > 0)
            composite.AddReward(new ExperienceReward(rewardDef.experience));
        if (rewardDef.gold > 0)
            composite.AddReward(new GoldReward(rewardDef.gold));
        if (rewardDef.items != null)
        {
            foreach (var item in rewardDef.items)
                composite.AddReward(new ItemReward(item.itemId, item.amount));
        }
        return composite;
    }
}