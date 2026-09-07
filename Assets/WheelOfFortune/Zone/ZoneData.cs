using UnityEngine;

namespace WheelOfFortune.Zone
{
    [CreateAssetMenu(menuName = "WheelOfFortune/Zone/ZoneData")]
    public class ZoneData : ScriptableObject
    {
        public int CurrentZone;
        public ZoneType CurrentZoneType;
        private int _safeInterval = 5;
        private int _superInterval = 30;

        public ZoneType CalculateZoneType(int zone)
        {
            if (zone % _superInterval == 0) return ZoneType.Super;
            if (zone % _safeInterval == 0) return ZoneType.Safe;
            return ZoneType.Normal;
        }

        public void IncrementZone()
        {
            CurrentZone++;
            CurrentZoneType = CalculateZoneType(CurrentZone);
        }

        public void Reset()
        {
            CurrentZone = 1;
            CurrentZoneType = CalculateZoneType(CurrentZone);
        }

    }
}