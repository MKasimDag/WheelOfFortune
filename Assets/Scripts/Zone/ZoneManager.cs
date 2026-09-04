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
            GameEventBus.Publish(new ZoneChangedEvent(_zoneData.CurrentZone, _zoneData.CurrentZoneType));
        }

        public void IncrementZone()
        {
            _zoneData.IncrementZone();
            GameEventBus.Publish(new ZoneChangedEvent(_zoneData.CurrentZone, _zoneData.CurrentZoneType));
        }
        private void OnEnable()
        {
            GameEventBus.Subscribe<SpinCompletedEvent>(OnSpinCompleted);
        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe<SpinCompletedEvent>(OnSpinCompleted);
        }
        private void OnSpinCompleted(SpinCompletedEvent e)
        {
            if (e.Result.IsBomb) {
                Debug.Log("Bomb hit! Resetting progress");
                ResetProgress();
                //IncrementZone();
            }
            else {
                IncrementZone();
                Debug.Log($"Spin completed. Going to next zone");
            }
        }
        private void OnDestroy()
        {
            ResetProgress();
        }
    }
}