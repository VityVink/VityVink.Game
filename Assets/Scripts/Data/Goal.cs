using UnityEngine;

[System.Serializable]
public abstract class Goal
{
    public abstract string Description { get; }      // описание цели для UI

    public abstract bool IsComplete(int currentProgress);
    public abstract string GetProgressText(int currentProgress);
    public abstract void UpdateProgress(ref int currentProgress, object eventData);
}
public abstract class ProgressGoal : Goal
{
    public int requiredAmount;  // поле здесь – общее для всех исчисляемых целей

    public override bool IsComplete(int currentProgress) => currentProgress >= requiredAmount;
    public override string GetProgressText(int currentProgress) => $"{currentProgress} / {requiredAmount}";
}
public abstract class FlagGoal : Goal
{
    public override bool IsComplete(int currentProgress) => currentProgress >= 1;
    public override string GetProgressText(int currentProgress) => currentProgress >= 1 ? "Выполнено" : "0 / 1";
}

[System.Serializable]
public class KillGoal : ProgressGoal
{
    public string monsterType;      // "Orc", "Goblin", "Dragon"
    
    public override string Description => $"Убить {monsterType} x{requiredAmount}";
    
    public override void UpdateProgress(ref int currentProgress, object eventData)
    {
        //if (eventData is OnMonsterKilledEvent evt && evt.monsterType == monsterType)
        //    currentProgress += evt.count;
    }
}


[System.Serializable]
public class CollectGoal : ProgressGoal
{
    public string itemId;           // ID предмета для сбора

    public override string Description => $"Собрать {itemId} x{requiredAmount}";

    public override void UpdateProgress(ref int currentProgress, object eventData)
    {
        //if (eventData is OnItemCollectedEvent evt && evt.itemId == itemId)
        //    currentProgress += evt.quantity;
    }
}


[System.Serializable]
public class TalkGoal : FlagGoal
{
    public string npcId;            // ID NPC для разговора
    
    public override string Description => $"Поговорить с {npcId}";

    public override void UpdateProgress(ref int currentProgress, object eventData)
    {
        //if (eventData is OnNPCTalkedEvent evt && evt.npcId == npcId)
        //    currentProgress = 1;
    }
}


[System.Serializable]
public class ReachLocationGoal : FlagGoal
{
    public string locationId;       // ID локации, которую нужно достичь

    public override string Description => $"Посетить локацию {locationId}";

    public override void UpdateProgress(ref int currentProgress, object eventData)
    {
        //if (eventData is OnLocationReachedEvent evt && evt.locationId == locationId)
        //    currentProgress = 1;
    }
}