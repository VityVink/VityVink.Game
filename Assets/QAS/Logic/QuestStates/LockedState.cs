using UnityEngine;

public class LockedState : IQuestState
{
    public void Start(IQuest quest)
    {
        Debug.Log($"[{quest.Title}] Заблокирован.");
    }

    public void Exit(IQuest quest)
    {
        // Ничего не делаем
    }

    public void Unlock(IQuest quest)
    {
        if (quest.IsUnlockConditionMet)
        {
            Debug.Log($"[{quest.Title}] Разблокирован!");
            EventBus.Instance.Publish(new QuestUnlockedEvent(quest));
            EventBus.Instance.Publish(new QuestsUpdatedEvent());
            quest.SetState(new AvailableState());
        }
        else
        {
            Debug.Log($"[{quest.Title}] Нельзя разблокировать – условие не выполнено.");
        }
    }

    public void Accept(IQuest quest)
    {
        Debug.Log($"[{quest.Title}] Нельзя взять – заблокирован.");
    }

    public void Complete(IQuest quest)
    {
        Debug.Log($"[{quest.Title}] Нельзя завершить – заблокирован.");
    }
}