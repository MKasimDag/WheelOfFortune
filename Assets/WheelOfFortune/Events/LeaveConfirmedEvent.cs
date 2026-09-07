// Events/LeaveConfirmedEvent.cs
using System.Collections.Generic;
using WheelOfFortune.Reward;

namespace WheelOfFortune.Events
{
    public readonly struct LeaveConfirmedEvent
    {
        public readonly IReadOnlyDictionary<string, RewardCacheData.Entry> CollectedRewards;
        public LeaveConfirmedEvent(IReadOnlyDictionary<string, RewardCacheData.Entry> collectedRewards)
        {
            CollectedRewards = collectedRewards;
        }
    }
}