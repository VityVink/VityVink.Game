using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private const string SAVE_FILE = "game_save.json";
    private string SavePath => Path.Combine(Application.persistentDataPath, SAVE_FILE);

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void SaveGame()
    {
        try
        {
            if (QuestManager.Instance == null)
            {
                Debug.LogError("QuestManager.Instance == null! Сохранение невозможно.");
                return;
            }

            var saveData = new GameSaveData();
            saveData.questsData = QuestManager.Instance.GetAllQuestsSaveData();

            string json = JsonUtility.ToJson(saveData, true);
            string encrypted = EncryptionHelper.Encrypt(json);
            File.WriteAllText(SavePath, encrypted);

            Debug.Log($"Игра сохранена: {SavePath}");
            Debug.Log($"    Количество квестов: {saveData.questsData?.Count ?? 0}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($" Ошибка сохранения: {e.Message}");
        }
    }

    public void LoadGame()
    {
        try
        {
            if (!File.Exists(SavePath))
            {
                Debug.Log("Файл сохранения не найден. Новая игра.");
                return;
            }

            string encrypted = File.ReadAllText(SavePath);
            string json = EncryptionHelper.Decrypt(encrypted);
            var saveData = JsonUtility.FromJson<GameSaveData>(json);

            if (saveData?.questsData != null && QuestManager.Instance != null)
            {
                QuestManager.Instance.LoadFromSaveData(saveData.questsData);
                Debug.Log($"Игра загружена: {SavePath}");
                Debug.Log($"   Количество квестов: {saveData.questsData.Count}");
            }
            else
            {
                Debug.LogWarning(" Данные сохранения пусты или повреждены.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Ошибка загрузки: {e.Message}");
            Debug.Log("Начинаем новую игру.");
        }
    }

    public void DeleteSave()
    {
        try
        {
            if (File.Exists(SavePath))
            {
                File.Delete(SavePath);
                Debug.Log($"Файл сохранения удалён: {SavePath}");
            }
            else
            {
                Debug.Log("Файла сохранения не существует.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Ошибка удаления: {e.Message}");
        }
    }

    public void ResetGame()
    {
        Debug.Log("Сброс игры...");

        DeleteSave();

        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.ResetAllQuests();
            Debug.Log("Квесты сброшены.");
        }

        // Очищаем уведомления
        NotificationPanelUI notificationPanel = FindObjectOfType<NotificationPanelUI>();
        if (notificationPanel != null)
        {
            notificationPanel.ClearAllNotifications();
            Debug.Log("Уведомления очищены.");
        }

        Debug.Log("Игра сброшена к начальному состоянию.");
    }
}