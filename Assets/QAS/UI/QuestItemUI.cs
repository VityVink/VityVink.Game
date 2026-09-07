using UnityEngine;
using UnityEngine.UI;

public class QuestItemUI : MonoBehaviour
{
    public Text titleText;
    public Text descriptionText;
    public Text progressText;
    public Image iconImage;
    public Button takeButton;

    public Color availableColor = Color.blue;
    public Color activeColor = Color.green;
    public Color completedColor = Color.gray;

    public void Setup(IQuest quest)
    {
        titleText.text = quest.Title;
        descriptionText.text = quest.Description;

        if (quest.CurrentState is CompletedState || quest.IsComplete)
        {
            progressText.text = "Выполнен";
            titleText.color = completedColor;
            if (takeButton != null) takeButton.gameObject.SetActive(false);
        }
        else if (quest.CurrentState is ActiveState)
        {
            progressText.text = $"{quest.CurrentAmount} / {quest.RequiredAmount}";
            titleText.color = activeColor;
            if (takeButton != null) takeButton.gameObject.SetActive(false);
        }
        else if (quest.CurrentState is AvailableState)
        {
            progressText.text = "Доступен";
            titleText.color = availableColor;
            if (takeButton != null)
            {
                takeButton.gameObject.SetActive(true);
                takeButton.onClick.RemoveAllListeners();
                takeButton.onClick.AddListener(() =>
                {
                    quest.Accept();
                    var journal = GetComponentInParent<QuestJournalUI>();
                    if (journal != null) journal.Refresh();
                });
            }
        }
        else
        {
            progressText.text = "Заблокирован";
            if (takeButton != null) takeButton.gameObject.SetActive(false);
        }
    }
}