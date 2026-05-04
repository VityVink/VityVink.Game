using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public void GrantReward(Reward reward)
    {
        switch (reward.type)
        {
            case RewardType.Gold:
                Debug.Log($"Награда: +{reward.amount} золота");
                return;
            case RewardType.Exp:
                Debug.Log($"Награда: +{reward.amount} опыта");
                return;
            case RewardType.Item:
                Debug.Log($"Награда: +{reward.itemID} x{reward.amount}");
                return;
        }
    }

    public void GrantRewards(List<Reward> rewards)
    {
        foreach (Reward reward in rewards)
        {
            GrantReward(reward);
        }
    }
}
