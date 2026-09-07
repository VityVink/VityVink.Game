using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<HealthChangedEvent>(OnHealthChanged);
    }

    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<HealthChangedEvent>(OnHealthChanged);
    }

    private void OnHealthChanged(HealthChangedEvent evt)
    {
        healthSlider.value = evt.CurrentHealth;
    }
}