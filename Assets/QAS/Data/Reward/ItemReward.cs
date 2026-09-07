using UnityEngine;

public class ItemReward : IReward
{
    private readonly string _itemId;
    private readonly int _quantity;

    public ItemReward(string itemId, int quantity = 1)
    {
        _itemId = itemId;
        _quantity = quantity;
    }

    public void Apply()
    {
        Debug.Log($"Получен предмет {_itemId} x{_quantity}");
    }

    public string GetDescription() => $"{_itemId} x{_quantity}";
}