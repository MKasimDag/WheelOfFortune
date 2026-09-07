using WheelOfFortune.Spin;

namespace WheelOfFortune.Events
{
    public readonly struct SpinResultEvent
    {
        public readonly SpinResult Result;
        public SpinResultEvent(SpinResult result) => Result = result;
    }
}