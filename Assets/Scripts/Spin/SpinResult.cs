namespace WheelOfFortune.Spin
{
    public readonly struct SpinResult
    {
        public readonly int SliceIndex;
        public readonly bool IsBomb;

        public SpinResult(int sliceIndex, bool isBomb)
        {
            SliceIndex = sliceIndex;
            IsBomb = isBomb;
        }
    }
}