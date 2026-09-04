using WheelOfFortune.Spin;

namespace WheelOfFortune.Events
{
    public readonly struct SpinCompletedEvent
    {
        public readonly SpinResult Result;

        public SpinCompletedEvent(SpinResult result)
        {
            Result = result;
        }
    }
}