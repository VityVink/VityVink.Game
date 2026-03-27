using System.Collections.Generic;
using System.Diagnostics;

public enum QuestState { Available, InProgress, Complete}
public class QuestInstance
{
    public QuestData QuestData;
    public List<int> CurrentAmmount;
    public QuestState QuestState;

    public QuestInstance(QuestData questData) 
    {
        this.QuestData = questData;
        CurrentAmmount = new List<int>();
        foreach (var task in questData.tasks)
        {
            CurrentAmmount.Add(0);
            Debug.WriteLine(CurrentAmmount);
        }
    }
}
