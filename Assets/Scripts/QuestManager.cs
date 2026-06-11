using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, QuestInfo> allQuestInfos = new();

    private void Start()
    {
        LoadAllQuestInfos();
    }

    private void LoadAllQuestInfos()
    {
        // Загрузка всех ассетов типа QuestInfo из папки Resources/Quests
        QuestInfo[] loadedQuests = Resources.LoadAll<QuestInfo>("Quests");

        foreach (var quest in loadedQuests)
        {
            if (!allQuestInfos.ContainsKey(quest.questId))
            {
                allQuestInfos.Add(quest.questId, quest);
                Debug.Log($"Загружен квест: {quest.questId}");
            }
            else
            {
                Debug.LogWarning($"Дубликат ID квеста: {quest.questId}");
            }
        }
    }
}