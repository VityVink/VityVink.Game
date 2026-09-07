using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestJournalUI : MonoBehaviour
{
    [Header("Контейнеры для списков")]
    public Transform availableContainer;
    public Transform activeContainer;
    public Transform completedContainer;

    [Header("Заголовки (опционально)")]
    public Text availableTitle;
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

        // Обновляем заголовки с количеством квестов
        UpdateTitles();

        ClearContainer(availableContainer);
        ClearContainer(activeContainer);
        ClearContainer(completedContainer);

        List<IQuest> allQuests = QuestManager.Instance.GetAllQuests();
        if (allQuests == null || allQuests.Count == 0)
        {
            Debug.Log("Нет зарегистрированных квестов.");
            return;
        }

        foreach (var quest in allQuests)
        {
            if (quest.IsAchievement)
                continue;

            Transform targetContainer = null;

            if (quest.CurrentState is AvailableState)
                targetContainer = availableContainer;
            else if (quest.CurrentState is ActiveState)
                targetContainer = activeContainer;
            else if (quest.CurrentState is CompletedState || quest.IsComplete)
                targetContainer = completedContainer;
            else
                continue;

            if (targetContainer == null) continue;

            GameObject go = Instantiate(questItemPrefab, targetContainer);
            QuestItemUI ui = go.GetComponent<QuestItemUI>();
            if (ui != null)
            {
                ui.Setup(quest);
            }
        }

        UpdateContainerSize(availableContainer);
        UpdateContainerSize(activeContainer);
        UpdateContainerSize(completedContainer);
    }

    private void UpdateTitles()
    {
        if (QuestManager.Instance == null) return;

        var allQuests = QuestManager.Instance.GetAllQuests();
        if (allQuests == null) return;

        int availableCount = 0, activeCount = 0, completedCount = 0;

        foreach (var q in allQuests)
        {
            if (q.IsAchievement) continue;

            if (q.CurrentState is AvailableState)
                availableCount++;
            else if (q.CurrentState is ActiveState)
                activeCount++;
            else if (q.CurrentState is CompletedState || q.IsComplete)
                completedCount++;
        }

        if (availableTitle != null)
            availableTitle.text = $"📌 Доступные ({availableCount})";
        if (activeTitle != null)
            activeTitle.text = $"⚡ Активные ({activeCount})";
        if (completedTitle != null)
            completedTitle.text = $"✅ Завершённые ({completedCount})";
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