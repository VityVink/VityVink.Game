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
                quest.Objective.Type == "kill" &&
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
}
