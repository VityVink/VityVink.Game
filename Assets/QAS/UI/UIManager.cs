using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI панели")]
    public GameObject questJournalPanel;
    public GameObject achievementsPanel;

    [Header("Элементы, которые скрываются при открытии панели")]
    public GameObject questTrackerPanel;
    public GameObject notificationPanel;

    [Header("Кнопки")]
    public GameObject journalButton;
    public GameObject achievementsButton;


    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        // Все выключаем
        SetAllUIObjectsActive(false);

        EventBus.Instance.Subscribe<GameStartedEvent>(OnGameStarted);
    }

    private void OnDestroy()
    {
        EventBus.Instance.Unsubscribe<GameStartedEvent>(OnGameStarted);
    }

    private void OnGameStarted(GameStartedEvent evt)
    {
        // Включаем всё
        SetAllUIObjectsActive(true);

        // Панели скрыты (открываются по кнопке)
        if (questJournalPanel != null)
            questJournalPanel.SetActive(false);
        if (achievementsPanel != null)
            achievementsPanel.SetActive(false);

        Debug.Log(" UI активирован после GameStartedEvent");
    }

    private void SetAllUIObjectsActive(bool active)
    {
        if (questJournalPanel != null) questJournalPanel.SetActive(active);
        if (achievementsPanel != null) achievementsPanel.SetActive(active);
        if (questTrackerPanel != null) questTrackerPanel.SetActive(active);
        if (notificationPanel != null) notificationPanel.SetActive(active);
        if (journalButton != null) journalButton.SetActive(active);
        if (achievementsButton != null) achievementsButton.SetActive(active);
    }

    private void ShowTrackerAndNotifications()
    {
        if (questTrackerPanel != null) questTrackerPanel.SetActive(true);
        if (notificationPanel != null) notificationPanel.SetActive(true);
    }

    public void ToggleJournal()
    {
        if (questJournalPanel == null) return;

        bool isOpen = questJournalPanel.activeSelf;

        // Закрываем всё
        if (questJournalPanel != null) questJournalPanel.SetActive(false);
        if (achievementsPanel != null) achievementsPanel.SetActive(false);

        if (isOpen)
        {
            // Если журнал был открыт — просто закрываем
            ShowTrackerAndNotifications();
        }
        else
        {
            // Открываем журнал
            questJournalPanel.SetActive(true);
        }
    }

    public void ToggleAchievements()
    {
        if (achievementsPanel == null) return;

        bool isOpen = achievementsPanel.activeSelf;

        // Закрываем всё
        if (questJournalPanel != null) questJournalPanel.SetActive(false);
        if (achievementsPanel != null) achievementsPanel.SetActive(false);

        if (isOpen)
        {
            ShowTrackerAndNotifications();
        }
        else
        {
            achievementsPanel.SetActive(true);
        }
    }
}