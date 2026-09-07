using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip deathSound;

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<PlayerDiedEvent>(OnPlayerDied);
    }

    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<PlayerDiedEvent>(OnPlayerDied);
    }

    private void OnPlayerDied(PlayerDiedEvent evt)
    {
        AudioSource.PlayClipAtPoint(deathSound, Vector3.zero);
    }
}