using UnityEngine;

public interface IQuestState 
{
    void Start(IQuest quest);
    void Exit(IQuest quest);
    void Unlock(IQuest quest);
    void Accept(IQuest quest);
    void Complete (IQuest quest);
}