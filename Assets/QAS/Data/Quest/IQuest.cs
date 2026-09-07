using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public interface IQuest
{
    string Id { get; }
    string Title { get; }
    string Description { get; }
    int RequiredAmount { get; }
    int CurrentAmount { get; }  
    IQuestState CurrentState { get; }
    bool IsComplete { get; }
    bool IsUnlockConditionMet { get; set; } 
    IReward Reward { get; set; }   
    bool IsAchievement { get; }
    string[] RequiredQuestIds { get; }
    bool IsTracking { get; }

    void Unlock();
    void Accept();
    void Complete();
    void SetState(IQuestState newState);  
    void AddProgress(int amount);   
    string GetProgress();
    void StartTracking();
    void StopTracking();
    QuestSaveData GetSaveData();
    void LoadSaveData(QuestSaveData data);
}
