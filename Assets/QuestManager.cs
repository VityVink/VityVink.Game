using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> Quests = new List<Quest>();

    public void OnEnemyKilled(string enemyID)
    {
        foreach (var quest in Quests)
        {
            if (quest.IsActive &&
                quest.Objectives.Contains.Type == "kill" &&
                quest.Objective.TargerID == enemyID)
            {
                quest.Objective.CurrentAmmount++;

                if (quest.Objective.IsComplete())
                {
                    quest.IsComplete = true;
                    Debug.Log($"Quest is complete: {quest.Name}");
                    GiveReward(quest);
                }
                else
                {
                    Debug.Log($"Quest Progress: {quest.Objective.CurrentAmmount}/{quest.Objective.RequiredAmount}");
                }

            }
        }
    }
    private void GiveReward(Quest quest)
    {
        Debug.Log($"Reward for quest is obtained: {quest.Name}");
    }
    private void Start()
    {
        Quest killRatsQuest = new Quest();
        killRatsQuest.ID = "kill_5_rats";
        killRatsQuest.Name = "Kill bad rats";
        killRatsQuest.IsActive = true;
        killRatsQuest.IsComplete = false;

        killRatsQuest.Objectives = new List<QuestObjective>()
        {
            new QuestObjective
            {
                Type = "kill",
                TargerID = "Rat",
                RequiredAmount = 5,
                CurrentAmmount = 0
            },
            new QuestObjective
            {
                Type = "collect",
                TargerID = "Sword",
                RequiredAmount = 3,
                CurrentAmmount = 0
            }
        };
        Quests.Add(killRatsQuest);
    }
}
