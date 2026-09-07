using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    private int health = 100;

    private void Update()
    {
        // Нажатие Space: увеличить счёт
        if (Input.GetKeyDown(KeyCode.Space))
        {
            score += 10;
            EventBus.Instance.Publish(new ScoreChangedEvent(score));
        }

        // Нажатие H: уменьшить здоровье и проверить смерть
        if (Input.GetKeyDown(KeyCode.H))
        {
            health -= 30;
            EventBus.Instance.Publish(new HealthChangedEvent(health));

            if (health <= 0)
            {
                EventBus.Instance.Publish(new PlayerDiedEvent("health reached zero"));
                // здесь можно выключить игру или перезагрузить сцену
            }
        }
    }
}