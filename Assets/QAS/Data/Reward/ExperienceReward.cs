using UnityEngine;

public class ExperienceReward : IReward
{
    private readonly int _amount;

    public ExperienceReward(int amount) => _amount = amount;

    public void Apply()
    {
        // Предположим, есть глобальный менеджер игрока
        Debug.Log($"Выдано {_amount} опыта.");
    }

    public string GetDescription() => $"+{_amount} XP";
}