using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<QuestInstance> activeQuests = new List<QuestInstance>();
    public List<QuestInstance> completeQuests = new List<QuestInstance>();
    public QuestData questData;
    public void TakeQuest(QuestData questData)
    {
        QuestInstance newQuest = new QuestInstance(questData);
        newQuest.state = QuestState.InProgress;
        activeQuests.Add(newQuest);
        Debug.Log($"{newQuest.data.title} is active");
    }
    private void GiveReward(QuestInstance quest)
    {
        Debug.Log($"Reward for quest is obtained: {quest.data.title}");
    }
    public void OnEnemyKilled(string enemyID)
    {
        foreach(var quest in activeQuests)
        {
            for (int i = 0; i < quest.data.tasks.Count; i++) 
            {
                var task = quest.data.tasks[i];
                if (task.type == TaskType.Kill && task.targerID == enemyID)
                {
                    quest.currentProgress[i]++;
                    Debug.Log($"{quest.data.title} is {quest.currentProgress[i]}/{quest.data.tasks[i].requiredAmount}");


                    if (quest.IsAllTaskComplete())
                    {
                        quest.state = QuestState.Complete;
                        GiveReward(quest);
                    }
                }
            }
        }
    }
    private void Start()
    {
        TakeQuest(questData);
    }

}

