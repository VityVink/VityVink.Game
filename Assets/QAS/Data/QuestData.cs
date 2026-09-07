using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "QAS/Quest Data")]
public class QuestData : ScriptableObject
{
    public string questId;
    public string title;
    [TextArea] public string description;
    public QuestType questType;
    public string targetId;
    public int requiredAmount = 1;
    public Reward reward;
    public bool isAchievement;
    public QuestData[] requiredQuests;
    public Sprite icon;
    public Color questColor = Color.white;
}