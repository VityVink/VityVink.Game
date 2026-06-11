using UnityEngine;

[System.Serializable]
public abstract class Reward
{
    public abstract string Description { get; }      // описание награды для UI
    
    public virtual string Validate() => null; // null = ошибок нет

    public abstract void Give();
}


[System.Serializable]
public class GoldReward : Reward
{
    public int amount;

    public override string Description => $"Золото x{amount}";

    public override string Validate()
    {
        if (amount <= 0) return "Gold amount must be > 0";
        return null;
    }

    public override void Give()
    {
        // Логика добавления золота игроку
        Debug.Log($"[Reward] Выдано золото: {amount}");
        //GameManager.Instance.AddGold(amount);
    }
}


[System.Serializable]
public class ExpReward : Reward
{
    public int amount;
    
    public override string Description => $"Опыт x{amount}";

    public override string Validate()
    {
        if (amount <= 0) return "Experience amount must be > 0";
        return null;
    }

    public override void Give()
    {
        Debug.Log($"[Reward] Выдано опыта: {amount}");
        //GameManager.Instance.AddExperience(amount);
    }
}


[System.Serializable]
public class ItemReward : Reward
{
    public string itemId;
    public int quantity;

    public override string Description => $"{itemId} x{quantity}";

    public override string Validate()
    {
        if (string.IsNullOrEmpty(itemId)) return "Item ID cannot be empty";
        if (quantity <= 0) return "Item quantity must be > 0";
        return null;
    }

    public override void Give()
    {
        Debug.Log($"[Reward] Выдан предмет: {itemId} в количестве {quantity}");
        //InventoryManager.Instance.AddItem(itemId, quantity);
    }
}