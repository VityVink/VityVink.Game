using UnityEngine;

public class AvailableState : IQuestState
{
    public void Start(IQuest quest)
    {
        Debug.Log($"[{quest.Title}] Доступен для взятия.");
        if (quest.IsAchievement)
            quest.Accept();

    }

    public void Exit(IQuest quest) { }

    public void Unlock(IQuest quest)
    {
        Debug.Log($"[{quest.Title}] Уже доступен.");
    }

    public void Accept(IQuest quest)
    {
        Debug.Log($"[{quest.Title}] Взят!");
        quest.SetState(new ActiveState());
        EventBus.Instance.Publish(new QuestAcceptedEvent(quest));
        EventBus.Instance.Publish(new QuestsUpdatedEvent());
    }

    public void Complete(IQuest quest)
    {
        Debug.Log($"[{quest.Title}] Нельзя завершить – сначала возьмите квест.");
    }
}