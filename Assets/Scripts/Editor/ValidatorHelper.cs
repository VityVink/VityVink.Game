using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ValidatorHelper
{
    public static List<string> ValidateAchievement(AchievementInfo achievement)
    {
        var errors = new List<string>();

        if (achievement == null) return errors;

        // ќб€зательные строковые пол€
        if (string.IsNullOrEmpty(achievement.achievementId))
            errors.Add("achievementId is empty");
        if (string.IsNullOrEmpty(achievement.title))
            errors.Add("title is empty");
        if (string.IsNullOrEmpty(achievement.description))
            errors.Add("description is empty");
        // description может быть пустым Ц по желанию
        // icon может быть опциональным

        // Goal
        if (achievement.goal == null)
            errors.Add("Goal is not assigned (null)");
        else
        {
            var goalError = achievement.goal.Validate();
            if (!string.IsNullOrEmpty(goalError))
                errors.Add($"Goal error: {goalError}");
        }

        return errors;
    }

    public static List<string> ValidateQuest(QuestInfo quest)
    {
        var errors = new List<string>();

        if (quest == null) return errors;

        // ќсновные пол€
        if (string.IsNullOrEmpty(quest.questId))
            errors.Add("questId is empty");
        if (string.IsNullOrEmpty(quest.questName))
            errors.Add("questName is empty");
        if (string.IsNullOrEmpty(quest.description))
            errors.Add("description is empty");
        // questIcon опционален

        // Goal
        if (quest.goal == null)
            errors.Add("Goal is not assigned (null)");
        else
        {
            var goalError = quest.goal.Validate();
            if (!string.IsNullOrEmpty(goalError))
                errors.Add($"Goal error: {goalError}");
        }

        // Rewards
        if (quest.rewards != null)
        {
            for (int i = 0; i < quest.rewards.Length; i++)
            {
                var reward = quest.rewards[i];
                if (reward == null)
                    errors.Add($"Reward at index {i} is null");
                else
                {
                    var rewardError = reward.Validate();
                    if (!string.IsNullOrEmpty(rewardError))
                        errors.Add($"Reward[{i}] error: {rewardError}");
                }
            }
        }

        // Prerequisites
        if (quest.prerequisites != null)
        {
            for (int i = 0; i < quest.prerequisites.Length; i++)
            {
                if (quest.prerequisites[i] == null)
                    errors.Add($"Prerequisite quest at index {i} is null");
            }
        }

        return errors;
    }
}