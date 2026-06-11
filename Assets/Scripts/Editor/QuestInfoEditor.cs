using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestInfo))]
public class QuestInfoEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var quest = (QuestInfo)target;
        var errors = ValidatorHelper.ValidateQuest(quest);

        if (errors.Count > 0)
        {
            EditorGUILayout.Space(10);
            var message = string.Join("\n", errors);
            EditorGUILayout.HelpBox(message, MessageType.Error);
        }
    }
}