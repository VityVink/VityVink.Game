
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "QAS/Quest")]
public class QuestInfo : ScriptableObject
{
    [Header("Основная информация")]
    public string questId;
    public string questName;
    [TextArea(3, 5)] public string description;
    public Sprite questIcon;

    [Header("Условия и награды")]
    [SerializeReference] public Goal goal;
    [SerializeReference] public Reward[] rewards;

    [Header("Дополнительно")]
    public bool isRepeatable;
    public QuestInfo[] prerequisites;
}
