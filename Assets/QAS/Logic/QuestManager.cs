using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    [Header("Ручная загрузка (инспектор)")]
    [SerializeField] private QuestData[] allQuestDatas; // можно оставить пустым

    private Dictionary<string, IQuest> _allQuests = new Dictionary<string, IQuest>();
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        // Загружаем квесты
        LoadQuestsFromResources();
        LoadQuestsFromCode();
        LoadAllQuestsFromSO();
        InitializeQuests();
        EventBus.Instance.Publish(new GameStartedEvent());
        
        // Загружаем сохранение
        SaveManager.Instance?.LoadGame();
    }

    // ==================== РЕГИСТРАЦИЯ И ПОЛУЧЕНИЕ ====================
    public void RegisterQuest(IQuest quest)
    {
        if (!_allQuests.ContainsKey(quest.Id))
        {
            _allQuests.Add(quest.Id, quest);
            NotifyQuestsChanged(); 
        }
        else
        {
            Debug.LogWarning($"Квест с ID {quest.Id} уже зарегистрирован!");
        }
    }

    public IQuest GetQuest(string questId)
    {
        _allQuests.TryGetValue(questId, out var quest);
        return quest;
    }

    public List<IQuest> GetAllQuests()
    {
        return new List<IQuest>(_allQuests.Values);
    }

    // ==================== ЗАВИСИМОСТИ ====================
    public bool IsQuestCompleted(string questId)
    {
        return _allQuests.TryGetValue(questId, out var quest) && quest.IsComplete;
    }

    public bool AreRequiredQuestsCompleted(IQuest quest)
    {
        foreach (var requiredId in quest.RequiredQuestIds)
        {
            if (!IsQuestCompleted(requiredId))
                return false;
        }
        return true;
    }

    public void OnQuestCompleted(IQuest completedQuest)
    {
        Debug.Log($"[QuestManager] Квест завершён: {completedQuest.Title}");

        foreach (var quest in _allQuests.Values)
        {
            if (quest.RequiredQuestIds.Contains(completedQuest.Id))
            {
                if (!quest.IsUnlockConditionMet && AreRequiredQuestsCompleted(quest))
                {
                    quest.IsUnlockConditionMet = true;
                    quest.Unlock();
                }
            }
        }
        NotifyQuestsChanged();
    }

    // ==================== ИНИЦИАЛИЗАЦИЯ ====================
    public void InitializeQuests()
    {
        foreach (var quest in _allQuests.Values)
        {
            if (quest.CurrentState is CompletedState) continue;
            if (quest.IsUnlockConditionMet) continue;

            if (AreRequiredQuestsCompleted(quest))
            {
                quest.IsUnlockConditionMet = true;
                quest.Unlock();
            }
        }
        NotifyQuestsChanged(); 
    }

    // ==================== ЗАГРУЗКА ИЗ RESOURCES ====================
    private void LoadQuestsFromResources()
    {
        QuestData[] questsData = Resources.LoadAll<QuestData>("QuestData");
        if (questsData == null || questsData.Length == 0)
        {
            Debug.Log("Нет квестов в Resources/QuestData (или папка отсутствует).");
            return;
        }

        Debug.Log($"Загружено {questsData.Length} квестов из Resources/QuestData");

        // Строим словарь зависимостей
        var dependencies = new Dictionary<string, string[]>();
        foreach (var data in questsData)
        {
            var reqIds = data.requiredQuests?.Select(q => q.questId).ToArray() ?? new string[0];
            dependencies[data.questId] = reqIds;
        }

        foreach (var data in questsData)
        {
            if (_allQuests.ContainsKey(data.questId))
            {
                Debug.LogWarning($"Квест {data.questId} уже зарегистрирован (пропускаем из Resources).");
                continue;
            }
            var quest = QuestFactory.CreateQuest(data, dependencies[data.questId]);
            RegisterQuest(quest);
        }
    }

    // ==================== ЗАГРУЗКА ИЗ ИНСПЕКТОРА (SO) ====================
    private void LoadAllQuestsFromSO()
    {
        var dependencies = new Dictionary<string, string[]>();
        foreach (var data in allQuestDatas)
        {
            if (data == null) continue;
            var reqIds = data.requiredQuests?.Select(q => q.questId).ToArray() ?? new string[0];
            dependencies[data.questId] = reqIds;
        }

        foreach (var data in allQuestDatas)
        {
            if (data == null) continue;
            if (_allQuests.ContainsKey(data.questId))
            {
                Debug.LogWarning($"Квест {data.questId} уже зарегистрирован (пропускаем из инспектора).");
                continue;
            }
            var quest = QuestFactory.CreateQuest(data, dependencies[data.questId]);
            RegisterQuest(quest);
        }
    }

    // ==================== ЗАГРУЗКА ИЗ КОДА (QuestDefinition) ====================
    private void LoadQuestsFromCode()
    {
        var definitions = AllQuests.GetAll(); // ваш статический массив из 50 квестов
        foreach (var def in definitions)
        {
            if (_allQuests.ContainsKey(def.id))
            {
                Debug.LogWarning($"Квест с ID {def.id} уже зарегистрирован (пропускаем из кода).");
                continue;
            }
            var quest = QuestFactory.CreateQuest(def);
            RegisterQuest(quest);
        }
    }

    // ==================== СОХРАНЕНИЕ ====================
    public List<QuestSaveData> GetAllQuestsSaveData()
    {
        var list = new List<QuestSaveData>();
        foreach (var quest in _allQuests.Values)
            list.Add(quest.GetSaveData());
        return list;
    }

    public void LoadFromSaveData(List<QuestSaveData> saveData)
    {
        foreach (var data in saveData)
        {
            if (_allQuests.TryGetValue(data.questId, out var quest))
                quest.LoadSaveData(data);
            else
                Debug.LogWarning($"Квест {data.questId} не найден при загрузке");
        }
        // Перепроверяем зависимости после загрузки
        InitializeQuests();
    }
    public void ResetAllQuests()
    {
        // Удаляем все квесты из словаря
        _allQuests.Clear();

        // Заново загружаем квесты (как при старте)
        LoadQuestsFromResources();
        if (allQuestDatas != null && allQuestDatas.Length > 0)
            LoadAllQuestsFromSO();
        LoadQuestsFromCode();

        // Инициализируем их заново
        InitializeQuests();
    }

    public void NotifyQuestsChanged()
    {
        EventBus.Instance.Publish(new QuestsUpdatedEvent());
    }
}
