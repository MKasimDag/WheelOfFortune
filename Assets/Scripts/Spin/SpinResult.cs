namespace WheelOfFortune.Spin
{
    public readonly struct SpinResult
    {
        public readonly int SliceIndex;
        public readonly bool IsBomb;
        public readonly int RewardAmount;

        public SpinResult(int sliceIndex, bool isBomb, int rewardAmount)
        {
            SliceIndex = sliceIndex;
            IsBomb = isBomb;
            RewardAmount = rewardAmount;
        }
    }
}