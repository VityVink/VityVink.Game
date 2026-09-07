// Маркерный интерфейс для всех событий
public interface IEvent { }

//public class DataResetEvent : IEvent
//{
//    public int NewScore { get; }
//    public ScoreChangedEvent(int newScore) => NewScore = newScore;
//}
public class ScoreChangedEvent : IEvent
{
    public int NewScore { get; }
    public ScoreChangedEvent(int newScore) => NewScore = newScore;
}

public class HealthChangedEvent : IEvent
{
    public int CurrentHealth { get; }
    public HealthChangedEvent(int currentHealth) => CurrentHealth = currentHealth;
}

public class PlayerDiedEvent : IEvent
{
    public string Reason { get; }
    public PlayerDiedEvent(string reason) => Reason = reason;
}

public class KillEntityEvent: IEvent
{
    public string EntityId { get; }
    public KillEntityEvent(string entityId) => EntityId = entityId;
}
public class CollectItemEvent : IEvent
{
    public string ItemId { get; }
    public int Amount { get; } // количество собранного за раз
    public CollectItemEvent(string itemId, int amount = 1)
    {
        ItemId = itemId;
        Amount = amount;
    }
}

public class ReachLocationEvent : IEvent
{
    public string LocationId { get; }
    public ReachLocationEvent(string locationId) => LocationId = locationId;
}

public class TalkNPCEvent : IEvent
{
    public string NPCId { get; }
    public TalkNPCEvent(string npcId) => NPCId = npcId;
}
public class AchievementUnlockedEvent : IEvent
{
    public string Title { get; }
    public AchievementUnlockedEvent(string title) => Title = title;
}
public class QuestAcceptedEvent : IEvent
{
    public IQuest Quest { get; }
    public QuestAcceptedEvent(IQuest quest) => Quest = quest;
}

public class QuestUnlockedEvent : IEvent
{
    public IQuest Quest { get; }
    public QuestUnlockedEvent(IQuest quest) => Quest = quest;
}

public class QuestCompletedEvent : IEvent
{
    public IQuest Quest { get; }
    public QuestCompletedEvent(IQuest quest) => Quest = quest;
}
public class QuestsUpdatedEvent : IEvent { }
public class GameStartedEvent : IEvent { }
