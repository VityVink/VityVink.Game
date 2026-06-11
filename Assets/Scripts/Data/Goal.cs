using UnityEngine;

[System.Serializable]
public abstract class Goal
{
    public string description;      // описание цели для UI
    public int requiredAmount;      // требуемое количество

    public abstract bool IsComplete(int currentProgress);
    public abstract string GetProgressText(int currentProgress);
    public abstract void UpdateProgress(ref int currentProgress, object eventData);
}


[System.Serializable]
public class KillGoal : Goal
{
    public string monsterType;      // "Orc", "Goblin", "Dragon"

    public override bool IsComplete(int currentProgress)
        => currentProgress >= requiredAmount;

    public override string GetProgressText(int currentProgress)
        => $"{currentProgress}/{requiredAmount}";

    public override void UpdateProgress(ref int currentProgress, object eventData)
    {
        //if (eventData is OnMonsterKilledEvent evt && evt.monsterType == monsterType)
        //    currentProgress += evt.count;
    }
}


[System.Serializable]
public class CollectGoal : Goal
{
    public string itemId;           // ID предмета для сбора

    public override bool IsComplete(int currentProgress)
        => currentProgress >= requiredAmount;

    public override string GetProgressText(int currentProgress)
        => $"{currentProgress}/{requiredAmount}";

    public override void UpdateProgress(ref int currentProgress, object eventData)
    {
        //if (eventData is OnItemCollectedEvent evt && evt.itemId == itemId)
        //    currentProgress += evt.quantity;
    }
}


[System.Serializable]
public class TalkGoal : Goal
{
    public string npcId;            // ID NPC для разговора

    public override bool IsComplete(int currentProgress)
        => currentProgress >= requiredAmount;

    public override string GetProgressText(int currentProgress)
        => currentProgress >= requiredAmount ? "Выполнено" : "Не выполнено";

    public override void UpdateProgress(ref int currentProgress, object eventData)
    {
        //if (eventData is OnNPCTalkedEvent evt && evt.npcId == npcId)
        //    currentProgress = 1;
    }
}


[System.Serializable]
public class ReachLocationGoal : Goal
{
    public string locationId;       // ID локации, которую нужно достичь

    public override bool IsComplete(int currentProgress)
        => currentProgress >= requiredAmount;

    public override string GetProgressText(int currentProgress)
        => currentProgress >= requiredAmount ? "Выполнено" : "Не выполнено";

    public override void UpdateProgress(ref int currentProgress, object eventData)
    {
        //if (eventData is OnLocationReachedEvent evt && evt.locationId == locationId)
        //    currentProgress = 1;
    }
}