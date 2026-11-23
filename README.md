# Quest & Event System — Unity

Система квестов, достижений и событий для Unity с использованием паттерна EventBus.

---

## Что это

Набор скриптов для:
- Квестов 4 типов: **Kill**, **Collect**, **Talk**, **Reach**
- Достижений (отдельный флаг `isAchievement`)
- Цепочек квестов (блокировка до выполнения предыдущих)
- Наград (XP, золото, предметы)
- Сохранения/загрузки прогресса
- UI: счёт, полоска здоровья

---

## Структура

### Ядро
| Файл | Назначение |
|---|---|
| `IQuest.cs` | Интерфейс квеста |
| `QuestType.cs` | Перечисление типов квестов |
| `QuestDefinition.cs` | Сериализуемое описание квеста (для `AllQuests`) |
| `QuestData.cs` | ScriptableObject-версия квеста |
| `QuestFactory.cs` | Создание квестов из `QuestDefinition` / `QuestData` |

### Реализации квестов
| Файл | Назначение |
|---|---|
| `KillEntityQuest.cs` | Убить N врагов типа X |
| `CollectItemQuest.cs` | Собрать N предметов типа X |
| `TalkNPCQuest.cs` | Поговорить с NPC N раз |
| `ReachLocationQuest.cs` | Посетить локацию N раз |

### Награды
| Файл | Назначение |
|---|---|
| `IReward.cs` | Интерфейс награды |
| `CompositeReward.cs` | Объединение нескольких наград |
| `ExperienceReward.cs` | Выдать XP |
| `GoldReward.cs` | Выдать золото |
| `ItemReward.cs` | Выдать предмет |

### Каталог контента
| Файл | Назначение |
|---|---|
| `AllQuests.cs` | Статический список 200+ квестов и достижений |

### UI и игровая логика (пример)
| Файл | Назначение |
|---|---|
| `ScoreUI.cs` | Отображение счёта |
| `HealthBar.cs` | Полоска здоровья |
| `AudioManager.cs` | Звук смерти |
| `AchievementSystem.cs` | Логика достижений |
| `GameManager.cs` | Тестовая логика (Space = +очки, H = -HP) |

---

## Установка

1. Скопируй все `.cs` файлы в `Assets/Scripts/`.
2. Убедись, что `EventBus` реализован и доступен как `EventBus.Instance`.
3. Создай сцену:
   - `Canvas` → `ScoreText` (Text) + `HealthSlider` (Slider)
   - Пустой объект с компонентами `ScoreUI`, `HealthBar`, `AudioManager`, `AchievementSystem`, `GameManager`
   - `EventSystem` на сцене (для кликов по кнопкам)

---

## Использование

### Создать квест

```csharp
var def = new QuestDefinition {
    id = "kill_goblin_3",
    title = "Гоблинская зачистка",
    description = "Убейте 3 гоблинов",
    type = QuestType.KillEntity,
    targetId = "goblin",
    requiredAmount = 3,
    reward = new Reward { experience = 30, gold = 15 }
};

IQuest quest = QuestFactory.CreateQuest(def);
quest.Unlock();
quest.Accept();
quest.StartTracking();  // подписка на события
```

### Публикация событий

```charp
EventBus.Instance.Publish(new KillEntityEvent("goblin"));
EventBus.Instance.Publish(new CollectItemEvent("berries", 1));
EventBus.Instance.Publish(new TalkNPCEvent("farmer"));
EventBus.Instance.Publish(new ReachLocationEvent("castle"));
```

### QuestSaveData data = quest.GetSaveData();

```charp
QuestSaveData data = quest.GetSaveData();
// ... сохранить в JSON ...
quest.LoadSaveData(data);
```
