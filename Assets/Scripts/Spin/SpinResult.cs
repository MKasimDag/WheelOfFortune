using UnityEngine;

namespace WheelOfFortune.Spin
{
    public readonly struct SpinResult
    {
        public readonly int SliceIndex;
        public readonly bool IsBomb;
        public readonly string RewardId;
        public readonly int RewardAmount;
        public readonly Sprite RewardIcon;
        

        public SpinResult(int sliceIndex, bool isBomb, string rewardId, int rewardAmount, Sprite rewardIcon)
        {
            SliceIndex = sliceIndex;
            IsBomb = isBomb;
            RewardId = rewardId;
            RewardAmount = rewardAmount;
            RewardIcon = rewardIcon;
        }
    }
}