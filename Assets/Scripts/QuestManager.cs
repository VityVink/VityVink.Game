using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Ссылки на другие системы")]
    [SerializeField] private PlayerActions playerActions;
    [SerializeField] private RewardManager rewardManager;

    [Header("Список квестов (для теста)")]
    [SerializeField] private List<Quest> activeQuests = new List<Quest>();

    private void Start()
    {
        // Если ссылки не заданы, то поиск совершается по сцене
        if (playerActions == null)
            playerActions = FindObjectOfType<PlayerActions>();
        if (rewardManager == null)
            rewardManager = FindObjectOfType<RewardManager>();

        // При наличии объекта происходит подписка на все события
        if (playerActions != null)
        {
            playerActions.OnAction += HandleAction; ;
            playerActions.OnTrigger += HandleTrigger; ;
            Debug.Log("QuestManager подписался на события");
        }
        else
            Debug.LogError($"QuestManager: PlayerAction не найден на сцене!");

        if (rewardManager == null)
            Debug.LogWarning("QuestManager: RewardManager не найден награды не будут выданы!");

        // Создание тестовых квестов
        CreateTestQuests();
    }

    // Обработчик событий с кол-вом
    private void HandleAction(string actionID, int amount)
    {
        Debug.Log($"QuestManager: получено действие '{actionID}' x{amount}");

        bool anyQuestUpdated = false;

        foreach (Quest quest in activeQuests)
        {
            // Выполненные квесты пропускаются
            if (quest.isCompleted) continue;

            // Проверка квеста под действие
            bool isMatching = false;

            if (quest.type == QuestType.Collect && quest.targetID == actionID)
            {
                isMatching = true;
            }
            else if (quest.type == QuestType.Kill && quest.targetID == actionID)
            {
                isMatching = true;
            }

            // Если этот квест подходит, то изменить его прогресс
            if (isMatching)
            {
                quest.AddProgress(amount);
                anyQuestUpdated = true;
                Debug.Log($"Обновлён квест: {quest.title} - {quest.GetProgressText()}");

                if (quest.isCompleted)
                    Debug.Log($"!!! КВЕСТ ВЫПОЛНЕН: {quest.title} !!!");
                if (rewardManager != null)
                    rewardManager.GrantRewards(quest.GetRewards());
            }
        }
        if (!anyQuestUpdated)
            Debug.Log("Ни один квест не обновлён");

        ShowAllQuestsStatus();
    }
    
    // Обработчик триггеров (разговоры, достижение точек)
    private void HandleTrigger(string triggerID)
    {
        Debug.Log($"QuestManager: получен триггер '{triggerID}'");

        bool anyQuestUpdated = false;

        foreach (Quest quest in activeQuests)
        {
            // Выполненные квесты пропускаются
            if (quest.isCompleted) continue;

            // Проверка квеста под триггер
            bool isMatching = false;

            if (quest.type == QuestType.TalkToNPC && quest.targetID == triggerID)
            {
                isMatching = true;
            }
            else if (quest.type == QuestType.ReachPoint && quest.targetID == triggerID)
            {
                isMatching = true;
            }

            // Если этот квест подходит, то изменить его прогресс
            if (isMatching)
            {
                quest.CompleteOnce();
                anyQuestUpdated = true;
                Debug.Log($"Завершён квест: {quest.title}");
                Debug.Log($"!!! КВЕСТ ВЫПОЛНЕН: {quest.title} !!!");
                if (rewardManager != null)
                    rewardManager.GrantRewards(quest.GetRewards());
            }
        }

        if (!anyQuestUpdated)
        {
            Debug.Log("Ни один квест не обновлён");
        }

        ShowAllQuestsStatus();
    }


    private void CreateTestQuests()
    {
        // Награда: 50 опыта и 10 золота за убийство гоблинов
        List<Reward> killRewards = new List<Reward>
    {
        new Reward(RewardType.Exp, 50),
        new Reward(RewardType.Gold, 10)
    };

        Quest killGoblins = new Quest(
            "kill_5_goblins",
            "Истребитель гоблинов",
            "Убейте 5 гоблинов",
            QuestType.Kill,
            "Goblin",
            5,
            killRewards
        );
        activeQuests.Add(killGoblins);

        // Награда: 100 золота за сбор
        List<Reward> goldRewards = new List<Reward>
    {
        new Reward(RewardType.Gold, 100)
    };

        Quest collectGold = new Quest(
            "collect_100_gold",
            "Богатство",
            "Соберите 100 золота",
            QuestType.Collect,
            "Gold",
            100,
            goldRewards
        );
        activeQuests.Add(collectGold);

        // Награда: предмет "SteelSword" за разговор с кузнецом
        List<Reward> talkRewards = new List<Reward>
    {
        new Reward("SteelSword", 1)
    };

        Quest talkBlacksmith = new Quest(
            "talk_to_blacksmith",
            "Новый клиент",
            "Поговорите с кузнецом",
            QuestType.TalkToNPC,
            "Blacksmith",
            1,
            talkRewards
        );
        activeQuests.Add(talkBlacksmith);

        // Награда: 20 опыта за достижение таверны
        List<Reward> reachRewards = new List<Reward>
    {
        new Reward(RewardType.Exp, 20)
    };

        Quest reachTavern = new Quest(
            "reach_tavern",
            "Отдохнуть",
            "Дойдите до таверны",
            QuestType.ReachPoint,
            "Tavern",
            1,
            reachRewards
        );
        activeQuests.Add(reachTavern);

        Debug.Log($"Создано {activeQuests.Count} тестовых квестов с наградами");
        ShowAllQuestsStatus();
    }




    // Показывает все квесты в консоли
    private void ShowAllQuestsStatus()
    {
        Debug.Log("ТЕКУЩИЙ ПРОГРЕСС КВЕСТОВ");
        foreach (Quest quest in activeQuests)
        {
            string status = quest.isCompleted ? "[X]" : "[]";
            Debug.Log($"{status} {quest.title}: {quest.GetProgressText()}");
        }
        Debug.Log("___________________");
    }
    // Удалить подписки при уничтожении
    private void OnDestroy()
    {
        // При наличии объекта происходит отписка от всех событий
        if (playerActions != null)
        {
            playerActions.OnAction -= HandleAction;
            playerActions.OnTrigger -= HandleTrigger;
        }
    }

}