using UnityEngine;

public class Reward
{
    public RewardType type;
    public int amount;
    public string itemID;

    // Конструктор для золота/опыта
    public Reward(RewardType type, int amount)
    {
        this.type = type;
        this.amount = amount;
        this.itemID = "";
    }

    // Конструктор для предметов
    public Reward(string itemID, int amount = 1)
    {
        this.type = RewardType.Item;
        this.itemID = itemID;
        this.amount = amount;
    }

}
