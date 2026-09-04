namespace WheelOfFortune.Spin
{
    public interface ISliceSelector
    {
        SpinResult Select(bool includesBomb);
    }
}