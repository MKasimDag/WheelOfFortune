using UnityEngine;

namespace WheelOfFortune.Zone
{
    public class ZoneManager : MonoBehaviour
    {
        [SerializeField] private ZoneData _zoneData;
        private int _safeInterval = 5;
        private int _superInterval = 30;

        private void Awake()
        {
            ResetProgress();
        }

        public void ResetProgress()
        {
            _zoneData.CurrentZone = 1;
            _zoneData.CurrentZoneType = CalculateZoneType(1);
        }

        public void IncrementZone()
        {
            _zoneData.CurrentZone++;
            _zoneData.CurrentZoneType = CalculateZoneType(_zoneData.CurrentZone);
        }
        private void OnDestroy()
        {
            ResetProgress();
        }

        private ZoneType CalculateZoneType(int zone)
        {
            if (zone % _superInterval == 0) return ZoneType.Super;
            if (zone % _safeInterval == 0) return ZoneType.Safe;
            return ZoneType.Normal;
        }
    }
}