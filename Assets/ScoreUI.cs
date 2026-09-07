using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<ScoreChangedEvent>(OnScoreChanged);
    }

    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<ScoreChangedEvent>(OnScoreChanged);
    }

    private void OnScoreChanged(ScoreChangedEvent evt)
    {
        scoreText.text = "Score: " + evt.NewScore;
    }
}