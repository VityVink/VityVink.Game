using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestTrackerUI : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private int maxDisplayCount = 3;

    [Header("UI")]
    [SerializeField] private Transform container;
    [SerializeField] private GameObject trackerItemPrefab;

    private List<GameObject> _items = new List<GameObject>();
    private bool _isSubscribed = false;

    private void OnEnable()
    {
        if (!_isSubscribed)
        {
            EventBus.Instance.Subscribe<QuestsUpdatedEvent>(OnQuestsUpdated);
            _isSubscribed = true;
        }
        Refresh();
    }

    private void OnDisable()
    {
        if (_isSubscribed)
        {
            EventBus.Instance.Unsubscribe<QuestsUpdatedEvent>(OnQuestsUpdated);
            _isSubscribed = false;
        }
    }

    private void OnQuestsUpdated(QuestsUpdatedEvent evt)
    {
        Refresh();
    }

    public void Refresh()
    {
        // Удаляем старые элементы
        foreach (var go in _items)
        {
            if (go != null) Destroy(go);
        }
        _items.Clear();

        if (QuestManager.Instance == null) return;

        var allQuests = QuestManager.Instance.GetAllQuests();
        if (allQuests == null || allQuests.Count == 0) return;

        // ✅ Только активные квесты (НЕ достижения)
        // ✅ Сортировка по прогрессу (сверху те, у кого прогресс больше)
        var activeQuests = allQuests
            .Where(q => !q.IsAchievement && q.CurrentState is ActiveState)
            .OrderByDescending(q => (float)q.CurrentAmount / q.RequiredAmount) // ⬅️ главное!
            .Take(maxDisplayCount)
            .ToList();

        // Создаём элементы
        foreach (var quest in activeQuests)
        {
            GameObject go = Instantiate(trackerItemPrefab, container);
            var item = go.GetComponent<QuestTrackerItem>();
            if (item != null)
                item.Setup(quest);
            _items.Add(go);
        }
    }
}