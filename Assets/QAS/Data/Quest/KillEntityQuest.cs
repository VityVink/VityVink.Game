using System;
using UnityEngine;

public class KillEntityQuest : IQuest
{
    // ----- Поля -----
    private string _id;
    private string _title;
    private string _description;
    private string _entityId;
    private int _requiredAmount;
    private int _currentAmount;
    private IQuestState _currentState;
    private bool _isUnlockConditionMet;
    private IReward _reward;
    private bool _isAchievement;
    private string[] _requiredQuestIds;
    private bool _isTracking = false;

    // ----- Конструктор -----
    public KillEntityQuest(
        string id,
        string title,
        string description,
        string entityId,
        int requiredAmount,
        IReward reward,
        bool isAchievement = false,
        string[] requiredQuestIds = null)
    {
        _id = id;
        _title = title;
        _description = description;
        _entityId = entityId;
        _requiredAmount = requiredAmount;
        _currentAmount = 0;
        _currentState = new LockedState();    // начальное состояние
        _isUnlockConditionMet = false;
        _reward = reward;
        _isAchievement = isAchievement;
        _requiredQuestIds = requiredQuestIds ?? Array.Empty<string>();

    }


    // ----- Реализация свойств IQuest -----
    public string Id => _id;
    public string Title => _title;
    public string Description => _description;
    public string EntityId => _entityId;
    public int RequiredAmount => _requiredAmount;

    public int CurrentAmount => _currentAmount;

    public IQuestState CurrentState => _currentState;

    public bool IsComplete => _currentAmount >= _requiredAmount;

    public bool IsUnlockConditionMet
    {
        get => _isUnlockConditionMet;
        set => _isUnlockConditionMet = value;
    }

    public IReward Reward
    {
        get => _reward;
        set => _reward = value;
    }

    public bool IsAchievement => _isAchievement;
    public string[] RequiredQuestIds => _requiredQuestIds;

    public bool IsTracking => _isTracking;
    // ----- Методы -----
    public void Unlock()
    {
        _currentState?.Unlock(this);
    }

    public void Accept()
    {
        _currentState?.Accept(this);
    }

    public void Complete()
    {
        _currentState?.Complete(this);
    }

    public void SetState(IQuestState newState)
    {
        _currentState?.Exit(this);
        _currentState = newState;
        _currentState?.Start(this);
    }

    public void AddProgress(int amount)
    {
        if (amount <= 0) return;

        _currentAmount = Mathf.Min(_currentAmount + amount, _requiredAmount);
        Debug.Log($"[{Title}] Прогресс: {_currentAmount}/{_requiredAmount}");
        EventBus.Instance.Publish(new QuestsUpdatedEvent());

        if (IsComplete && _currentState is ActiveState)
            Complete();

    }

    public string GetProgress()
    {
        return $"{_currentAmount}/{_requiredAmount}";
    }
    public void StartTracking()
    {
        if (_isTracking) return;
        _isTracking = true;
        EventBus.Instance.Subscribe<KillEntityEvent>(OnKillEntity);
    }

    public void StopTracking()
    {
        if (!_isTracking) return;
        _isTracking = false;
        EventBus.Instance.Unsubscribe<KillEntityEvent>(OnKillEntity);
    }

    private void OnKillEntity(KillEntityEvent evt)
    {
        if (evt.EntityId == _entityId)
        {
            AddProgress(1);
        }
    }
    public QuestSaveData GetSaveData()
    {
        return new QuestSaveData
        {
            questId = _id,
            stateType = _currentState.GetType().Name,
            currentAmount = _currentAmount,
            isUnlockConditionMet = _isUnlockConditionMet
        };
    }

    public void LoadSaveData(QuestSaveData data)
    {
        _currentAmount = data.currentAmount;
        _isUnlockConditionMet = data.isUnlockConditionMet;

        // Восстанавливаем состояние
        switch (data.stateType)
        {
            case nameof(LockedState):
                _currentState = new LockedState();
                break;
            case nameof(AvailableState):
                _currentState = new AvailableState();
                break;
            case nameof(ActiveState):
                _currentState = new ActiveState();
                if (_currentState is ActiveState active)
                {
                    active.Start(this); // подписываемся на события
                }
                break;
            case nameof(CompletedState):
                _currentState = new CompletedState();
                break;
            default:
                _currentState = new LockedState();
                break;
        }
    }
}