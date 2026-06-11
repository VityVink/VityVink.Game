using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AchievementInfo))]
public class AchievementInfoEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Рисуем стандартный инспектор
        DrawDefaultInspector();

        // Валидация
        var achievement = (AchievementInfo)target;
        var errors = ValidatorHelper.ValidateAchievement(achievement);

        if (errors.Count > 0)
        {
            EditorGUILayout.Space(10);
            var message = string.Join("\n", errors);
            EditorGUILayout.HelpBox(message, MessageType.Error);
        }
    }
}