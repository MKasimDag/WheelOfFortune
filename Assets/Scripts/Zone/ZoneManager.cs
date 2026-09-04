using UnityEngine;
using WheelOfFortune.Events;

namespace WheelOfFortune.Zone
{
    public class ZoneManager : MonoBehaviour
    {
        [SerializeField] private ZoneData _zoneData;

        private void Awake()
        {
            ResetProgress();
        }

        public void ResetProgress()
        {
            _zoneData.Reset();
        }

        public void IncrementZone()
        {
            _zoneData.IncrementZone();
            GameEventBus.Publish(new ZoneChangedEvent(_zoneData.CurrentZone, _zoneData.CurrentZoneType));
        }
        private void OnDestroy()
        {
            ResetProgress();
        }
    }
}