
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "QAS/Quest")]
public class QuestInfo : ScriptableObject
{
    [Header("Основная информация")]
    public string questId;
    public string questName;
    [TextArea] public string description;
    public Sprite questIcon;

    [Header("Условия и награды")]
    [SerializeReference] public Goal goal;
    [SerializeReference] public Reward[] rewards;

    [Header("Дополнительно")]
    public QuestInfo[] prerequisites;

    public string Validate()
    {
        if (goal == null)
            return "Goal: не выбран тип цели";
        string goalError = goal.Validate();
        if (!string.IsNullOrEmpty(goalError))
            return $"Goal: {goalError}";

        if (rewards == null || rewards.Length == 0)
            return "Rewards: добавьте хотя бы одну награду";
        for (int i = 0; i < rewards.Length; i++)
        {
            if (rewards[i] == null)
                return $"Rewards[{i}]: не выбран тип награды";
            string rewardError = rewards[i].Validate();
            if (!string.IsNullOrEmpty(rewardError))
                return $"Rewards[{i}]: {rewardError}";
        }
        return null;
    }
}
