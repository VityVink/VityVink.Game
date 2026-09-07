using UnityEngine;

public class ToggleJournal : MonoBehaviour
{
    [Header("—сылка на панель журнала")]
    public GameObject journalPanel; // панель с компонентом QuestJournalUI
    [SerializeField] private KeyCode actionKey;


    private void Update()
    {
        if (Input.GetKeyDown(actionKey))
            Toggle();
    }

    public void Toggle()
    {
        if (journalPanel == null)
        {
            Debug.LogWarning("Journal Panel не назначен в ToggleJournal!");
            return;
        }

        bool isActive = !journalPanel.activeSelf;
        journalPanel.SetActive(isActive);

        // ≈сли панель открываетс€ Ц обновл€ем содержимое
        if (isActive)
        {
            QuestJournalUI journal = journalPanel.GetComponent<QuestJournalUI>();
            if (journal != null)
                journal.Refresh();
        }
    }

}