public enum TaskType
{
    Kill,
    Collect,
    Talk,
    ReachCoordinate
}
public class QuestObjective
{
   

    public TaskType Type;
    public string TargerID;
    public int RequiredAmount;
    public int CurrentAmmount;

    public bool IsComplete() => CurrentAmmount >= RequiredAmount;
}
[System.Serializable]
public class TaskData
{
    public TaskType type;
    public string targerID;
    public int requiredAmount;
    public string description;
}