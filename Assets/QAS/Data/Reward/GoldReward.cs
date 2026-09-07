using UnityEngine;

public class GoldReward : IReward
{
    private readonly int _amount;

    public GoldReward(int amount) => _amount = amount;

    public void Apply()
    {
        Debug.Log($"Выдано {_amount} золота.");
    }

    public string GetDescription() => $"+{_amount} золота";
}