using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPanelUI : MonoBehaviour
{
    [Header("UI")]
    public Transform notificationContainer;
    public GameObject notificationPrefab;

    [Header("Настройки")]
    public float displayDuration = 3f;
    public float fadeDuration = 0.5f;

    private Queue<GameObject> _notificationQueue = new Queue<GameObject>();
    private bool _isShowing = false;

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<QuestUnlockedEvent>(OnQuestUnlocked);
        EventBus.Instance.Subscribe<QuestAcceptedEvent>(OnQuestAccepted);
        EventBus.Instance.Subscribe<QuestCompletedEvent>(OnQuestCompleted);
    }

    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<QuestUnlockedEvent>(OnQuestUnlocked);
        EventBus.Instance.Unsubscribe<QuestAcceptedEvent>(OnQuestAccepted);
        EventBus.Instance.Unsubscribe<QuestCompletedEvent>(OnQuestCompleted);
    }

    private void OnQuestUnlocked(QuestUnlockedEvent evt)
    {
        string prefix = evt.Quest.IsAchievement ? "🏆" : "📌";
        string type = evt.Quest.IsAchievement ? "Достижение" : "Квест";
        ShowNotification($"{prefix} {type} разблокирован: {evt.Quest.Title}", Color.cyan);
    }

    private void OnQuestAccepted(QuestAcceptedEvent evt)
    {
        string prefix = evt.Quest.IsAchievement ? "🏆" : "📌";
        string type = evt.Quest.IsAchievement ? "Достижение" : "Квест";
        ShowNotification($"{prefix} {type} взят: {evt.Quest.Title}", Color.green);
    }

    private void OnQuestCompleted(QuestCompletedEvent evt)
    {
        string prefix = evt.Quest.IsAchievement ? "🏆" : "📌";
        string type = evt.Quest.IsAchievement ? "Достижение" : "Квест";
        ShowNotification($"✅ {prefix} {type} завершён: {evt.Quest.Title}", Color.yellow);
    }

    private void ShowNotification(string message, Color color)
    {
        GameObject go = Instantiate(notificationPrefab, notificationContainer);
        Text text = go.GetComponentInChildren<Text>();
        if (text != null)
        {
            text.text = message;
            text.color = color;
        }

        CanvasGroup cg = go.GetComponent<CanvasGroup>();
        if (cg == null) cg = go.AddComponent<CanvasGroup>();
        cg.alpha = 1f;

        _notificationQueue.Enqueue(go);

        if (!_isShowing)
            StartCoroutine(ProcessNotifications());
    }

    private IEnumerator ProcessNotifications()
    {
        _isShowing = true;

        while (_notificationQueue.Count > 0)
        {
            GameObject go = _notificationQueue.Dequeue();
            if (go == null) continue;

            go.SetActive(true);
            yield return new WaitForSeconds(displayDuration);

            CanvasGroup cg = go.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                float timer = 0f;
                while (timer < fadeDuration)
                {
                    timer += Time.deltaTime;
                    cg.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
                    yield return null;
                }
            }

            Destroy(go);

            if (notificationContainer != null)
                LayoutRebuilder.ForceRebuildLayoutImmediate(notificationContainer as RectTransform);

            yield return new WaitForSeconds(0.2f);
        }

        _isShowing = false;
    }

    // ===== МЕТОД ОЧИСТКИ (ВЫЗЫВАЕТСЯ ИЗВНЕ) =====
    public void ClearAllNotifications()
    {
        // Останавливаем обработку очереди
        StopAllCoroutines();
        _isShowing = false;

        // Очищаем очередь
        _notificationQueue.Clear();

        // Удаляем все дочерние объекты в контейнере
        if (notificationContainer != null)
        {
            foreach (Transform child in notificationContainer)
            {
                if (child != null)
                    Destroy(child.gameObject);
            }
        }

        Debug.Log("[NotificationPanelUI] Все уведомления очищены.");
    }
}