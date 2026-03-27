using System.Collections.Generic;
public enum QuestState
{
    Available,
    InProgress,
    Complete
}
public class Quest
{
    public string ID;
    public string Title;
    public List<QuestObjective> Objectives;
    public QuestState State;
    public bool IsComplete;
    public bool IsActive;
}

public class QuestInstance
{
    public QuestData data;
    public QuestState state;
    public List<int> currentProgress;

    public QuestInstance(QuestData data)
    {
        this.data = data;
        this.state = QuestState.Available;

        currentProgress = new List<int>();
        foreach (var task in data.tasks)
        {
            currentProgress.Add(0);
        }
    }

    public bool IsAllTaskComplete()
    {
        for (int i = 0; i < data.tasks.Count; i++)
        {
            if (currentProgress[i] < data.tasks[i].requiredAmount)
            {
                return false;
            }
        }
        return true;
    }
}
