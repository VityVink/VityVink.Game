public class QuestObjective
{
    public string Type;
    public string TargerID;
    public int RequiredAmount;
    public int CurrentAmmount;

    public bool IsComplete() => CurrentAmmount > RequiredAmount;
}
