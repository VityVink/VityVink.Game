using UnityEngine;

public class ToggleAchievementWindows: MonoBehaviour
{
    [Header("—сылка на панель журнала")]
    public GameObject achievementWindow; // панель с компонентом QuestJournalUI
    [SerializeField] private KeyCode actionKey;


    private void Update()
    {
        if (Input.GetKeyDown(actionKey))
            Toggle();
    }

    public void Toggle()
    {
        if (achievementWindow == null)
        {
            Debug.LogWarning("Achievement Window не назначен в ToggleJournal!");
            return;
        }

        bool isActive = !achievementWindow.activeSelf;
        achievementWindow.SetActive(isActive);

        // ≈сли панель открываетс€ Ц обновл€ем содержимое
        if (isActive)
        {
            AchievementWindowUI window = achievementWindow.GetComponent<AchievementWindowUI>();
            if (window != null)
                window.Refresh();
        }
    }

}