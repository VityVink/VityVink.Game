using System.Collections.Generic;
using System.Diagnostics;

public enum QuestState { Available, InProgress, Complete}
public class QuestInstance
{
    public QuestData QuestData;
    public List<int> CurrentAmount;
    public QuestState QuestState;

    public QuestInstance(QuestData questData) 
    {
        QuestData = questData;
        CurrentAmount = new List<int>();
        foreach (var task in questData.tasks)
        {
            CurrentAmount.Add(0);
        }
        QuestState = QuestState.Available;
    }
    public bool IsAllTaskComplete()
    {
        for (int i = 0; i < CurrentAmount.Count; i++)
        {
            if (CurrentAmount[i] < QuestData.tasks[i].RequiredAmount)
            {
                return false;
            }
        }
        return true;
    }
}
