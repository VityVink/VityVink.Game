using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestData questData1;
    public List<QuestInstance> CurrentQuests = new List<QuestInstance>();
    public List<QuestInstance> CompleteQuests = new List<QuestInstance>();
    
    public void TakeQuest(QuestData questData)
    {
        QuestInstance newQuest = new QuestInstance(questData);
        newQuest.QuestState = QuestState.InProgress;
        CurrentQuests.Add(newQuest);
        Debug.Log($"Quest {questData.Title} is active");
    }
    public void OnEnemyKilled(string enemyName)
    {
        foreach (var quest in CurrentQuests)
        {
            for (int i = 0; i < quest.QuestData.tasks.Count; i++)
            {
                var task = quest.QuestData.tasks[i].TargerName;
                if (quest.QuestState == QuestState.InProgress &&
                    enemyName == task &&
                    quest.QuestData.tasks[i].Type == TaskType.Kill)
                {
                    quest.CurrentAmount[i]++;
                    if (quest.CurrentAmount[i] > quest.QuestData.tasks[i].RequiredAmount)
                    {
                        continue;
                    }
                    else
                    {
                        Debug.Log($"Quest {quest.QuestData.Title} progress: " +
                            $"{quest.CurrentAmount[i]}/" +
                            $"{quest.QuestData.tasks[i].RequiredAmount}!");
                    }
                }
                if (quest.IsAllTaskComplete())
                {
                    Debug.Log($"Quest {quest.QuestData.Title} is complete!");
                    quest.QuestState = QuestState.Complete;
                    GetReward();
                    CompleteQuests.Add(quest);
                }
            }
        }
        foreach (var quest in CompleteQuests)
        {
            CurrentQuests.Remove(quest);
        }
    }
    public void GetReward()
    {
        Debug.Log("Quest is rewarded!");
    }
    void Start()
    {
        TakeQuest(questData1);
    }
}
