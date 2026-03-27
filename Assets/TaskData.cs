using Unity.VisualScripting;

public enum TaskType { Kill, Collect, TalkNPC, ReachPoint}

[System.Serializable]
public class TaskData
{
    public TaskType Type;
    public string TargerName;
    public int RequiredAmount;
}
