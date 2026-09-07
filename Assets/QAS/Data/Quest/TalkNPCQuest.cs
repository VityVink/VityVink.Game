using System;
using UnityEngine;

public class TalkNPCQuest : IQuest
{
    private string _id;
    private string _title;
    private string _description;
    private string _npcId;
    private IQuestState _currentState;
    private bool _isUnlockConditionMet;
    private bool _talked;
    private IReward _reward;
    private bool _isAchievement;
    private string[] _requiredQuestIds;
    private bool _isTracking = false;


    public TalkNPCQuest(
        string id,
        string title,
        string description,
        string npcId,
        IReward reward,
        bool isAchievement = false,
        string[] requiredQuestIds = null)
    {
        _id = id;
        _title = title;
        _description = description;
        _npcId = npcId;
        _currentState = new LockedState();
        _isUnlockConditionMet = false;
        _talked = false;
        _reward = reward;
        _isAchievement = isAchievement;
        _requiredQuestIds = requiredQuestIds ?? Array.Empty<string>();

    }
    public string Id => _id;
    public string Title => _title;
    public string Description => _description;
    public string NPCId => _npcId;
    public int RequiredAmount => 1;
    public int CurrentAmount => _talked ? 1 : 0;
    public IQuestState CurrentState => _currentState;
    public bool IsComplete => _talked;
    public bool IsUnlockConditionMet { get => _isUnlockConditionMet; set => _isUnlockConditionMet = value; }
    public IReward Reward
    {
        get => _reward;
        set => _reward = value;
    }
    public bool IsAchievement => _isAchievement;
    public string[] RequiredQuestIds => _requiredQuestIds;
    public bool IsTracking => _isTracking;

    public void Unlock() => _currentState?.Unlock(this);
    public void Accept() => _currentState?.Accept(this);
    public void Complete() => _currentState?.Complete(this);
    public void SetState(IQuestState newState)
    {
        _currentState?.Exit(this);
        _currentState = newState;
        _currentState?.Start(this);
    }

    public void AddProgress(int amount) { }


    public string GetProgress()
    {
        if (_talked)
        {
            QuestManager.Instance?.NotifyQuestsChanged();

            EventBus.Instance.Publish(new QuestsUpdatedEvent());

            return "1/1";
        }
        else
            return "0/1";
    }


    public void StartTracking()
    {
        if (_isTracking) return;
        _isTracking = true;
        EventBus.Instance.Subscribe<TalkNPCEvent>(OnTalkNPC);
    }

    public void StopTracking()
    {
        if (!_isTracking) return;
        _isTracking = false;
        EventBus.Instance.Unsubscribe<TalkNPCEvent>(OnTalkNPC);
    }

    private void OnTalkNPC(TalkNPCEvent evt)
    {
        if (!_talked && evt.NPCId == _npcId)
        {
            _talked = true;
            Debug.Log($"[{Title}] Разговор с {_npcId} выполнен.");
            if (_currentState is ActiveState)
                Complete();
        }
    }
    public QuestSaveData GetSaveData()
    {
        return new QuestSaveData
        {
            questId = _id,
            stateType = _currentState.GetType().Name,
            currentAmount = _talked ? 1 : 0,
            isUnlockConditionMet = _isUnlockConditionMet
        };
    }

    public void LoadSaveData(QuestSaveData data)
    {
        _talked = data.currentAmount >= 1;
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
                    active.Start(this);
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