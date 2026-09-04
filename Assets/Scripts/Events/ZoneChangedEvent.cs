using WheelOfFortune.Zone;

namespace WheelOfFortune.Events
{
    public readonly struct ZoneChangedEvent
    {
        public readonly int Zone;
        public readonly ZoneType Type;

        public ZoneChangedEvent(int zone, ZoneType type)
        {
            Zone = zone;
            Type = type;
        }
    }
}