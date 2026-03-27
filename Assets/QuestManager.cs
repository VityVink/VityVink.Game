using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestData questData1;
    public List<QuestInstance> CurrentQuests = new List<QuestInstance>();
    
    public void TakeQuest(QuestData questData)
    {
        QuestInstance newQuest = new QuestInstance(questData);
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
                if (quest.QuestData.tasks[i].Type == TaskType.Kill && enemyName == task)
                {
                    quest.CurrentAmmount[i]++;
                    Debug.Log($"Quest {quest.QuestData.Title} progress: " +
                        $"{quest.CurrentAmmount[i]}/" +
                        $"{quest.QuestData.tasks[i].RequiredAmmount}!");
                    if (quest.IsAllTaskComplete())
                    {
                        Debug.Log($"Quest {quest.QuestData.Title} is complete!");
                    }
                }
            }
        }
    }
    void Start()
    {
        TakeQuest(questData1);
    }

    void Update()
    {
        
    }
}
