using UnityEngine;

public class QuestTester : MonoBehaviour
{
    void Update()
    {
        // Сохранение и загрузка
        if(Input.GetKeyDown(KeyCode.F5))
        {
            Debug.Log("fsfs");
            SaveManager.Instance.SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            SaveManager.Instance.LoadGame();
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            SaveManager.Instance.ResetGame();
        }

        //if (UIManager.Instance != null && UIManager.Instance.IsUIOpen)
        //    return;
        // Раздатчики для квестов
        // Kill
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EventBus.Instance.Publish(new KillEntityEvent("goblin"));
            Debug.Log("Гоблин убит");
        
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            EventBus.Instance.Publish(new KillEntityEvent("dragon"));
            Debug.Log("Дракон убит");

        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            EventBus.Instance.Publish(new KillEntityEvent("skeleton"));
            Debug.Log("Скелет убит");

        }

        // Collect
        if (Input.GetKeyDown(KeyCode.W))
        {
            int value = Random.Range(1, 5);
            EventBus.Instance.Publish(new CollectItemEvent("berries", value));
            Debug.Log($"Собрано ягод x{value}");

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            EventBus.Instance.Publish(new CollectItemEvent("gold_nugget"));
            Debug.Log($"Собран золотой самородок x1");

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            int value = 3;
            EventBus.Instance.Publish(new CollectItemEvent("egg", value));
            Debug.Log($"Собрано яиц x{value}");

        }

        // TalkNPC
        if (Input.GetKeyDown(KeyCode.E))
        {
            EventBus.Instance.Publish(new TalkNPCEvent("farmer"));
            Debug.Log("Встретились с Фермером");

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            EventBus.Instance.Publish(new TalkNPCEvent("blacksmith"));
            Debug.Log("Встретились с Кузнецом");

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            EventBus.Instance.Publish(new TalkNPCEvent("librarian"));
            Debug.Log("Встретились с Библиотекарем");

        }

        // ReachLocation
        if (Input.GetKeyDown(KeyCode.R))
        {
            EventBus.Instance.Publish(new ReachLocationEvent("castle"));
            Debug.Log("Прийшли в замок");

        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            EventBus.Instance.Publish(new ReachLocationEvent("home"));
            Debug.Log("Прийшли в дом");

        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            EventBus.Instance.Publish(new ReachLocationEvent("village"));
            Debug.Log("Прийшли в деревню");

        }

    }

}