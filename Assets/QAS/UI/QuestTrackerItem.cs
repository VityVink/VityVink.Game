using UnityEngine;
using UnityEngine.UI;

public class QuestTrackerItem : MonoBehaviour
{
    public Text titleText;
    public Text descriptionText;
    public Text progressText;
    public Slider progressSlider;

    public void Setup(IQuest quest)
    {
        if (titleText != null) titleText.text = quest.Title;
        if (descriptionText != null) descriptionText.text = quest.Description;

        if (progressText != null)
        {
            if (quest.IsComplete || quest.CurrentState is CompletedState)
                progressText.text = "✅ Выполнен";
            else if (quest.CurrentState is ActiveState)
                progressText.text = $"{quest.CurrentAmount} / {quest.RequiredAmount}";
            else
                progressText.text = "⏳ ...";
        }

        if (progressSlider != null)
        {
            if (quest.IsComplete || quest.CurrentState is CompletedState)
            {
                progressSlider.value = 1f;
            }
            else if (quest.CurrentState is ActiveState && quest.RequiredAmount > 0)
            {
                progressSlider.value = (float)quest.CurrentAmount / quest.RequiredAmount;
            }
            else
            {
                progressSlider.value = 0f;
            }
        }
    }
}