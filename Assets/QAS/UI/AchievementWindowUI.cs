using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementWindowUI : MonoBehaviour
{
    [Header("Контейнеры для списков")]
    public Transform activeContainer;
    public Transform completedContainer;

    [Header("Заголовки (опционально)")]
    public Text activeTitle;
    public Text completedTitle;

    [Header("Префаб элемента")]
    public GameObject questItemPrefab;

    [Header("Кнопка закрытия (опционально)")]
    public Button closeButton;

    private void Awake()
    {
        if (closeButton != null)
            closeButton.onClick.AddListener(() => gameObject.SetActive(false));
    }

    private void OnEnable()
    {
        Refresh();
    }

    private void OnDisable()
    {
        if (closeButton != null)
            closeButton.onClick.RemoveAllListeners();
    }

    public void Refresh()
    {
        ClearContainer(activeContainer);
        ClearContainer(completedContainer);

        List<IQuest> allQuests = QuestManager.Instance.GetAllQuests();
        if (allQuests == null || allQuests.Count == 0)
        {
            Debug.Log("Нет зарегистрированных достижений.");
            return;
        }

        foreach (var quest in allQuests)
        {
            if (!quest.IsAchievement)
                continue;

            Transform targetContainer = null;

            if (quest.CurrentState is ActiveState)
                targetContainer = activeContainer;
            else if (quest.CurrentState is CompletedState || quest.IsComplete)
                targetContainer = completedContainer;
            else
                continue;

            if (targetContainer == null) continue;

            GameObject go = Instantiate(questItemPrefab, targetContainer);
            QuestItemUI ui = go.GetComponent<QuestItemUI>();
            if (ui != null) ui.Setup(quest);
        }

        UpdateTitles();

        UpdateContainerSize(activeContainer);
        UpdateContainerSize(completedContainer);
    }

    private void UpdateTitles()
    {
        if (QuestManager.Instance == null) return;

        var allQuests = QuestManager.Instance.GetAllQuests();
        if (allQuests == null) return;

        int activeCount = 0, completedCount = 0;

        foreach (var q in allQuests)
        {
            if (!q.IsAchievement) continue;

            if (q.CurrentState is ActiveState)
                activeCount++;
            else if (q.CurrentState is CompletedState || q.IsComplete)
                completedCount++;
        }

        if (activeTitle != null)
            activeTitle.text = $"🏆 Активные достижения ({activeCount})";
        if (completedTitle != null)
            completedTitle.text = $"✅ Завершённые достижения ({completedCount})";
    }

    private void ClearContainer(Transform container)
    {
        if (container == null) return;
        foreach (Transform child in container)
            Destroy(child.gameObject);
    }

    private void UpdateContainerSize(Transform container)
    {
        if (container == null) return;
        LayoutRebuilder.ForceRebuildLayoutImmediate(container as RectTransform);
    }
}