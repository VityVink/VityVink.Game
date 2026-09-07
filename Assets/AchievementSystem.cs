using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.Instance.Subscribe<ScoreChangedEvent>(OnScoreChanged);
        EventBus.Instance.Subscribe<PlayerDiedEvent>(OnPlayerDied);
    }

    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<ScoreChangedEvent>(OnScoreChanged);
        EventBus.Instance.Unsubscribe<PlayerDiedEvent>(OnPlayerDied);
    }

    private void OnScoreChanged(ScoreChangedEvent evt)
    {
        if (evt.NewScore >= 100)
            Debug.Log("Achievement unlocked: 100 points!");
    }

    private void OnPlayerDied(PlayerDiedEvent evt)
    {
        Debug.Log($"Achievement failed: Player died because of {evt.Reason}");
    }
}