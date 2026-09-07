using UnityEngine;

public class CompletedState : IQuestState
{
    public void Start(IQuest quest)
    {
        quest.Reward?.Apply();
        if (quest.IsAchievement)
            EventBus.Instance.Publish(new AchievementUnlockedEvent(quest.Title));

        QuestManager.Instance?.OnQuestCompleted(quest);

        SaveManager.Instance?.SaveGame();
    }

    public void Exit(IQuest quest) { }

    public void Unlock(IQuest quest)
    {
        Debug.Log($"[{quest.Title}] Уже завершён.");
    }

    public void Accept(IQuest quest)
    {
        Debug.Log($"[{quest.Title}] Нельзя взять – уже выполнен.");
    }

    public void Complete(IQuest quest)
    {
        Debug.Log($"[{quest.Title}] Уже завершён.");
    }
}