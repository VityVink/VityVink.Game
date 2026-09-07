using UnityEngine;

public class ActiveState : IQuestState
{
    public void Start(IQuest quest)
    {
        Debug.Log($"[{quest.Title}] Активен. Выполняйте цели.");
        quest.StartTracking();

    }

    public void Exit(IQuest quest)
    {
        quest.StopTracking();
    }

    public void Unlock(IQuest quest)
    {
        Debug.Log($"[{quest.Title}] Уже активен.");
    }

    public void Accept(IQuest quest)
    {
        Debug.Log($"[{quest.Title}] Уже взят.");
    }

    public void Complete(IQuest quest)
    {
        if (quest.IsComplete)
        {
            Debug.Log($"[{quest.Title}] Завершён! Получите награду.");
            quest.SetState(new CompletedState());
            EventBus.Instance.Publish(new QuestCompletedEvent(quest));
            EventBus.Instance.Publish(new QuestsUpdatedEvent());
        }
        else
        {
            Debug.Log($"[{quest.Title}] Цели ещё не выполнены. Текущий прогресс: {quest.CurrentAmount}/{quest.RequiredAmount}");
        }
    }
}