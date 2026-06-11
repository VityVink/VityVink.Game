using UnityEngine;

[System.Serializable]
public abstract class Reward
{
    public string description;      // описание награды для UI

    public abstract void Give();
}


[System.Serializable]
public class GoldReward : Reward
{
    public int amount;

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

    public override void Give()
    {
        Debug.Log($"[Reward] Выдан предмет: {itemId} в количестве {quantity}");
        //InventoryManager.Instance.AddItem(itemId, quantity);
    }
}