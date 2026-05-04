using System;
using System.Collections.Generic;

[Serializable]

public class Quest
{
    // Статические данные (задаются при создании)
    public string id;
    public string title;
    public string description;
    public QuestType type;
    public string targetID;
    public int targetAmount;
    public List<Reward> rewards;

    // Динамические данные
    public int currentAmount;
    public bool isCompleted;

    // Конструктор (для удобного создания квестов)
    public Quest(string id, string title, string description, QuestType type, string targetID, int targetAmount, List<Reward> rewards = null)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.type = type;
        this.targetID = targetID;
        this.targetAmount = targetAmount;
        this.rewards = rewards ?? new List<Reward>(); // если null - пустой список Reward

        this.currentAmount = 0;
        this.isCompleted = false;
    }

    // Метод для обновления прогресса с кол-вом
    public void AddProgress(int amount)
    {
        if (isCompleted) return;

        currentAmount += amount;

        if (currentAmount >= targetAmount)
        {
            currentAmount = targetAmount;   // Фиксация на максимуме
            isCompleted = true;
        }
    }

    // Метод для обновления триггеров
    public void CompleteOnce()
    {
        if (isCompleted) return;

        currentAmount = 1;
        isCompleted = true;
    }

    // Получить текущий прогресс задания
    public string GetProgressText()
    {
        if (type == QuestType.ReachPoint || type == QuestType.TalkToNPC)
            return isCompleted ? "Выполнен" : "Не выполнен";
        return $"{currentAmount}/{targetAmount}";
    }
    
    // Получить награду за выполнение задания
    public List<Reward> GetRewards()
    {
        return rewards;
    }
}

