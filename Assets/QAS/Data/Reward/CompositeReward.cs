using System.Collections.Generic;
using System.Linq;

public class CompositeReward : IReward
{
    private readonly List<IReward> _rewards = new();

    public CompositeReward AddReward(IReward reward)
    {
        _rewards.Add(reward);
        return this;
    }

    public void Apply()
    {
        foreach (var reward in _rewards)
            reward.Apply();
    }

    public string GetDescription() => string.Join(", ", _rewards.Select(r => r.GetDescription()));
}