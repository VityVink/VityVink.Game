using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "QAS/Achievement")]
public class AchievementInfo : ScriptableObject
{
    public string achievementId;
    public string title;
    [TextArea] public string description;
    public Sprite icon;
    [SerializeReference] public Goal goal;   // переиспользуем ту же цель
    public bool isHidden;                          // скрытое достижение
}