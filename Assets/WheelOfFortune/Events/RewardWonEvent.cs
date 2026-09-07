using UnityEngine;

namespace WheelOfFortune.Events
{
    public readonly struct RewardWonEvent
    {
        public readonly string RewardId;
        public readonly int Amount;
        public readonly Sprite Icon;

        public RewardWonEvent(string rewardId, int amount, Sprite icon)
        {
            RewardId = rewardId;
            Amount = amount;
            Icon = icon;
        }
    }
}